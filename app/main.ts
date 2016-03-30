requirejs.config({
    paths: {
        'text': '../Scripts/text',
        'jquery': '../Scripts/jquery-2.1.1',
        'durandal': '../Scripts/durandal',
        'plugins': '../Scripts/durandal/plugins',        
        'knockout': '../Scripts/knockout-3.2.0'
    }
});

define(['bootstrapper'], bootstrapper=> {
    bootstrapper.run();
}); 