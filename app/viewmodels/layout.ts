import router = require('plugins/router');

class LayoutViewModel {
    router: DurandalRouter;

    activate() {
        console.log(window.location);
        router.map([
            { route: '*details', title: 'Wedding Invite', moduleId: 'viewmodels/home', nav: false },
        ]);
        router.mapUnknownRoutes('viewmodels/notFound', 'notfound');
        return router.activate({ routeHandler: router.loadUrl, root: '', pushState: true });
    }

    constructor() {
        this.router = router;
    }


    scrollToSection(sectionID) {      
      $('html, body').animate({
        scrollTop: $("#" + sectionID).offset().top
      }, 1000);
      return false;
    }


}

export = LayoutViewModel;
