var App = Ember.Application.create();

// Router
App.Router.map(function () {
    this.resource('makemodels');
});

App.ApplicationRoute = Ember.Route.extend({
    setupController: function () {
        this.controllerFor('makeModels').set('model', []);
        this.controllerFor('makeModels').set('allMakeModels', App.MakeModel.find());
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

App.Store = DS.Store.extend({
    revision: 11,
    adapter: 'DS.FixtureAdapter'
});

App.MakeModel = DS.Model.extend({
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