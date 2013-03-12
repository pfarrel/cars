var App = Ember.Application.create();

App.ChartView = Ember.View.extend({
  chart: {}
  ,line: {}
  ,area: {}

  ,updateChart: function updateChart() {
    var content = this.get('content');
    var chart = this.get('chart');
    var line = this.get('line');
    var area = this.get('area');

    chart.selectAll('path.line')
    .data(content)
      .transition()
    .duration(500)
    .ease('sin')
    .attr('d', line(content));
    chart.selectAll('path.area')
    .data(content)
    .transition()
    .duration(500)
    .ease('sin')
    .attr('d', area(content));
    }.observes('content.@each.value')

  ,didInsertElement: function didInsertElement() {
    var elementId = this.get('elementId');
    var content = this.get('content');

    var margin = { top: 35, right: 35, bottom: 35, left: 35};
    var w = 500 - margin.right - margin.left;
    var h = 300 - margin.top - margin.top;

    var x = d3.scale.linear()
    .range([0,w])
    .domain([1,content.length]);

    var y = d3.scale.linear()
    .range([h,0])
    .domain([0,100]);

    var xAxis = d3.svg.axis()
    .scale(x)
    .ticks(10)
    .tickSize(-h)
    .tickSubdivide(true);

    var yAxis = d3.svg.axis()
    .scale(y)
    .ticks(4)
    .tickSize(-w)
    .orient('left');

    var line = d3.svg.line()
        .interpolate('monotone')
        .x(function(d) { return x(d.get('timestamp'))})
        .y(function(d) { return y(d.get('value'))});
    this.set('line',line);

    var area = d3.svg.area()
        .interpolate('monotone')
        .x(function(d) { return x(d.get('timestamp')); })
        .y0(h)
        .y1(function(d) { return y(d.get('value')); });
    this.set('area',area);

    var chart = d3.select('#'+elementId).append('svg:svg')
        .attr('id','chart')
        .attr('width', w+margin.left+margin.right)
        .attr('height', w+margin.top+margin.bottom)
        .append('svg:g')
        .attr('transform', 'translate('+margin.left+','+margin.top+')');


    chart.append('svg:g')
        .attr('class', 'x axis')
        .attr('transform', 'translate(0,' + h + ')')
        .call(xAxis);

    chart.append('svg:g')
        .attr('class', 'y axis')
        .call(yAxis);

    chart.append('svg:clipPath')
        .attr('id', 'clip')
        .append('svg:rect')
        .attr('width', w)
        .attr('height', h);

    chart.append('svg:path')
        .attr('class', 'area')
        .attr('clip-path', 'url(#clip)')
        .attr('d', area(content));

    chart.append('svg:path')
        .attr('class', 'line')
        .attr('clip-path', 'url(#clip)')
        .attr('d', line(content));
    this.set('chart',chart);
  }
});

App.ChartValuesController = Ember.ArrayController.create({
    content: []
    ,init: function init() {
        this.replaceWithRandom();
    }
    ,replaceWithRandom: function replaceWithRandom() {
        var newContent = [];
        var max = 100;

        for(var i = 0, l = 100; i < l; i++) {
            var item = Ember.Object.create({
                timestamp: i
                ,value: max/2+Math.sin(i)*Math.ceil((max/2.5)*Math.random())
            });

            newContent[i] = item;
        }

        this.set('content', newContent);
    }
});

App.ApplicationView = Ember.View.extend({
  chartValuesBinding: 'App.ChartValuesController.content'
});

App.ApplicationController = Ember.Controller.extend({
  generateNewChartValues: function(event) {
    App.ChartValuesController.replaceWithRandom();
  }

});


App.Router.map(function () {
});

App.ApplicationRoute = Ember.Route.extend({
  setupController: function () {
    this.controllerFor('makeModels').set('model', []);
    this.controllerFor('makeModels').set('allMakeModels', App.MakeModel.find());
    this.controllerFor('locations').set('model', []);
    this.controllerFor('locations').set('allLocations', App.Location.find());
  }
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
