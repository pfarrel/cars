var App = Ember.Application.create();

App.Router.map(function () {
});

App.ApplicationRoute = Ember.Route.extend({
  setupController: function () {
    this.controllerFor('makeModels').set('model', []);
    this.controllerFor('makeModels').set('allMakeModels', App.MakeModel.find());
    this.controllerFor('locations').set('model', []);
    this.controllerFor('locations').set('allLocations', App.Location.find());
    loadData(this.controllerFor('chart'));
  }
});


App.ChartView = Ember.View.extend({
  updateChart: function updateChart() {
    var data = this.get('controller.filteredData');
    var chart = this.get('chart');
    // chart not first rendered yet
    if (chart === undefined) { return; }

    var width = this.get('width'),
        height = this.get('height');
    var x = d3.scale.linear()
        .range([0, width]);

    var y = d3.scale.linear()
        .range([height, 0]);

    var color = d3.scale.category10();

    var xAxis = d3.svg.axis()
        .scale(x)
        .orient("bottom");

    var yAxis = d3.svg.axis()
        .scale(y)
        .orient("left");

    var yearExtents = d3.extent(data, function (d) { return d.Year; });
    yearExtents[0]--;
    yearExtents[1]++;

    x.domain(yearExtents);
    y.domain(d3.extent(data, function (d) { return d.Price; })).nice();

    chart.select("g.xaxis").remove();
    chart.select("g.yaxis").remove();
    chart.append("g")
        .attr("class", "xaxis")
        .attr("transform", "translate(0," + height + ")")
        .call(xAxis)
      .append("text")
        .attr("class", "label")
        .attr("x", width)
        .attr("y", -6)
        .style("text-anchor", "end")
        .text("Year");

    chart.append("g")
        .attr("class", "yaxis")
        .call(yAxis)
      .append("text")
        .attr("class", "label")
       .attr("transform", "rotate(-90)")
        .attr("y", 6)
        .attr("dy", ".71em")
        .style("text-anchor", "end")
        .text("Price")

    var circles = chart.selectAll("circle")
      .data(data, function (d) { return d.Price; });

    circles.enter().append("circle")
      .attr("r", 3.5)
      .attr("cx", function (d) { return 0; })

    circles.transition().duration(1000)
      .attr("cy", function (d) { return y(d.Price); })
      .attr("cx", function (d) { return x(d.Year); })

    circles.exit().transition()
      .duration(1000)
      .attr("cx", 0).remove()
    }.observes('controller.filteredData')

  ,didInsertElement: function didInsertElement() {
    var elementId = this.get('elementId');
    var margin = { top: 30, right: 30, bottom: 40, left: 80 },
    width = 960 - margin.left - margin.right,
    height = 500 - margin.top - margin.bottom;
    this.set('width', width);
    this.set('height', height);

    var chart = d3.select('#'+elementId).append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
      .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
    this.set('chart', chart);

    }
});

App.ChartController = Ember.ArrayController.extend({
  content: [],
  needs: ['locations'],
  allData: [],
  filteredData: function() {
    var locations = this.get('controllers.locations.content');
    var matches = this.get('allData').filter(function (d) {
      return locations.some(function (location) {
        return d.Location === location.get('name');
      }) && d.Price < 100000 && d.Year > 1995;
    });
    return matches;
  }.property('allData', 'controllers.locations.content.@each'),
});

App.MakeModelsController = Ember.ArrayController.extend({
  search: '',

  searchedContent: function () {
    var regexp = new RegExp(this.get('search'), 'i');
    var controller = this;
    return this.get('allMakeModels').filter(function(item) {
      return !controller.contains(item) && regexp.test(item.get('name'));
    });
  }.property('search', 'model.@each', 'allMakeModels.@each'),    // Depend on allMakeModels so that this is computed on initial load

  addMakeModel: function (makeModel) {
    this.addObject(makeModel);
  },
  removeMakeModel: function (makeModel) {
    this.removeObject(makeModel);
  }
});

App.LocationsController = Ember.ArrayController.extend({
  search: '',

  searchedContent: function () {
    var regexp = new RegExp(this.get('search'), 'i');
    var controller = this;
    return this.get('allLocations').filter(function(item) {
      return !controller.contains(item) && regexp.test(item.get('name'));
    });
  }.property('search', 'model.@each', 'allLocations.@each'),

  addLocation: function (location) {
    this.addObject(location);
  },
  removeLocation: function (location) {
    this.removeObject(location);
  }
});

App.Store = DS.Store.extend({
  revision: 11,
  adapter: 'DS.FixtureAdapter'
});

App.MakeModel = DS.Model.extend({
  name: DS.attr('string')
});

App.Location = DS.Model.extend({
  name: DS.attr('string')
});

App.MakeModel.FIXTURES = [
  { id: 1, name: 'Ford Focus' },
  { id: 2, name: 'Renault Megane' },
  { id: 3, name: 'Vauxhall Astra' },
  { id: 4, name: 'Peugot 206' },
  { id: 5, name: 'Volkswagen Touran' },
  { id: 6, name: 'Toyota Corolla' }
];

App.Location.FIXTURES = [
  { id: 1, name: 'Dublin' },
  { id: 2, name: 'Wicklow' },
  { id: 3, name: 'Cork' },
  { id: 4, name: 'Galway' },
  { id: 5, name: 'Waterford' },
  { id: 6, name: 'Limerick' }
];

function loadData(controller) {
  d3.json("/data.js", function (error, data) {
    data.forEach(function (d) {
      d.Price = +d.Price;
      d.Year = +d.Year;
      d.Mileage = +d.Mileage;
    });
    controller.set('allData', data);
  });;
}

