var App = Ember.Application.create();

// Router
App.Router.map(function () {
    this.resource('makemodels');
});

App.ApplicationRoute = Ember.Route.extend({
    setupController: function () {
        this.controllerFor('makemodels').set('model', App.Makemodel.find());
        this.controllerFor('selectedmakemodels').set('model', []);
    }
});

App.MakemodelsController = Ember.ArrayController.extend({
    addMakeModel: function (makeModel) {
        var selectedController = this.controllerFor('selectedmakemodels')
            .get('model')
            .addObject(makeModel);
    }
});

App.SelectedmakemodelsController = Ember.ArrayController.extend({
    removeMakeModel: function (makeModel) {
        this.removeObject(makeModel);
    }
});

//App.SelectedMakeModelsController = Ember.ArrayController.extend();

App.Store = DS.Store.extend({
    revision: 11,
    adapter: 'DS.FixtureAdapter'
});

App.Makemodel = DS.Model.extend({
    name: DS.attr('string')
});

App.Makemodel.FIXTURES = [
    { id: 1, name: 'Ford - Focus' },
    { id: 2, name: 'Renault - Megane' },
    { id: 3, name: 'Vauxhall - Astra' },
    { id: 4, name: 'Peugot - 206' },
    { id: 5, name: 'Volkswagen - Touran' },
    { id: 6, name: 'Toyota - Corolla' },
];