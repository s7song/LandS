import system = require('durandal/system');
import app = require('durandal/app');
import viewLocator = require('durandal/viewLocator');

class Bootstrapper {
    static run() {
        //>>excludeStart("build", true);
        system.debug(true);
        //>>excludeEnd("build");

        app.title = 'Linus + Shuk Wedding';

        app.configurePlugins({
            router: true
        });


        app.start().then(() => {
            //Replace 'viewmodels' in the moduleId with 'views' to locate the view.
            //Look for partial views in a 'views' folder in the root.
            viewLocator.useConvention();

            //Show the app by setting the root view model for our application with a transition.
            app.setRoot('viewmodels/layout');
        });
    }
}

export = Bootstrapper;