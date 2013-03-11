var App = Ember.Application.create();

// Router
App.Router.map(function () {
    this.resource('makemodels');
    this.resource('locations');
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
    }.property('search', 'model.@each', 'allLocations.@each'),    // Depend on allMakeModels so that this is computed on initial load

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
