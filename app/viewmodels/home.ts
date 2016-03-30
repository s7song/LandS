import ko = require("knockout");
import system = require("durandal/system");

class HomePageViewModel {
    message = ko.observable("It would be an honour for you to join us at our wedding ceremony as we witness before God.");
    name = ko.observable("");
    intro = ko.computed(() => this.name() ? this.name() : "Hey there");
    invitedToReception = ko.observable(false);
    isComing: KnockoutObservable<boolean> = ko.observable(null);
    multiplePeople = ko.observable(false);
    rsvpView = ko.computed(() => this.invitedToReception() ? this.isComing() == null ? "rsvp.html" : this.isComing() ? "coming.html" : "notComing.html" : null);
    remarks = ko.observable("");
    htmlRemarks = ko.computed(() => this.remarks().replace(new RegExp("\n", "g"), "<br/>"));
    url = "";
    inEditRemarksMode = ko.observable(false);
    decided = ko.computed(() => this.isComing() != null);
    apiBaseUrl = "http://shukandlinus.com/";
    originalRemarks = ko.observable("");
    showCancelEdit = ko.computed(() => this.originalRemarks() != "");
    loading = ko.observable(true);
    minLoadTimePromise: JQueryPromise<any>;

    constructor() {
        this.isComing.subscribe(() => {
            this.rsvp();
            this.inEditRemarksMode(true);
        });

        this.minLoadTimePromise = system.defer(dfd => setTimeout(() => dfd.resolve(), 1500)).promise();
    }

    activate(context) {
        if (context) {
            this.url = context;
            localStorage.setItem("url", this.url);
        }
        else {
            this.url = localStorage.getItem("url");
        }

        if (!this.url)
            this.url = "generic";                                  

        var fetchDataPromise = this.fetchData(this.url).then(x => {
            return x ? x : this.fetchData("generic");
        });

        $.when(this.minLoadTimePromise, fetchDataPromise).done(() => {
            $('#loading').css("border", "0");
            $('#loading').addClass('animated bounceOut');
            $('#loading').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', () => {
                this.loading(false);
            });
        });
    }

    fetchData(url: string) {
        return $.get(this.apiBaseUrl + "invite/" + url)
            .then(response => {
                if (!response)
                    return null;

                this.message(response.message);
                this.name(response.name);
                this.isComing(response.isComing);
                this.multiplePeople(response.multiplePeople);
                this.remarks(response.remarks);
                this.invitedToReception(response.invitedToReception);
                this.inEditRemarksMode(!this.remarks());
                this.originalRemarks(response.remarks);
                return response;
            });
    }

    rsvp() {
        $.ajax({
            url: this.apiBaseUrl + "invite/" + this.url,
            data: {
                "isComing": this.isComing(),
            },
            type: "post"
        });
    }

    toggleEditMode() {
        this.inEditRemarksMode(true);
    }

    cancelEditMode() {
        this.inEditRemarksMode(false);
        this.remarks(this.originalRemarks());
    }

    submitRemarks() {
        this.inEditRemarksMode(false);
        this.originalRemarks(this.remarks());
        $.ajax({
            url: this.apiBaseUrl + "invite/" + this.url,
            data: {
                "remarks": this.remarks()
            },
            type: "post"
        }).fail(() => {
                alert("Something went wrong while updating your remarks, please try again.");
                this.inEditRemarksMode(true);
            });
    }

    imComing() {
        this.isComing(true);
    }

    notComing() {
        this.isComing(false);
    }
}
export = HomePageViewModel;