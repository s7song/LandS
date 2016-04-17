/**
* AnimatedScroll.js v1.1.5
* Smooth, animated document scroll to a specific element, supporting native jQuery UI easings.
* https://github.com/yevhentiurin/animatedscrolljs
*
* By Yevhen Tiurin <yevhentiurin@gmail.com>, Mikhail Semakhin <mike.semakhin@gmail.com>, Carlo Alberto Ferraris <cafxx@cafxx.strayorange.com>
* Licensed under the LGPL Version 3 license.
* http://www.gnu.org/licenses/lgpl.txt
*
* Packed with Dean Edwards JavaScript Compressor version 3.0
* http://dean.edwards.name/packer/
*
* Date: December 2, 2013
**/

$(function () {
    $('a[href*=#]:not([href=#])').click(function () {
        var target = $(this.hash);
        target = target.length ? target : $('[name=' + this.hash.substr(1) + ']');
        if (target.length) {
            $('html,body').animate({
                scrollTop: target.offset().top
            }, 1000);
            return false;
        }
    });
});