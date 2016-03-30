/*
 * Custom v1.0
 * DoveThemes.com
 *
 * Copyright (c) 2013-2014 DoveThemes.com
 *
 * License: GNU General Public License v2 or later
 * http://www.gnu.org/licenses/gpl-2.0.html
 *
 */

( function( $ ) {
			
	var match = {

		// Menu
		menuInit: function() {

			// Superfish Menu
			$( 'ul.sf-menu' ).superfish({
				delay: 			1200,
				animation: 		{ opacity : 'show', height : 'show' },
				speed: 			'fast',
				autoArrows: 	false,
				cssArrows: 		true 
			});

			// Mobile Menu
			$( '.sitebar' ).after( '<div id="primary-menu-toggle"></div>' );
			$( '.primary-menu' ).slicknav({
				prependTo: 		'#primary-menu-toggle'
			});

		},

		// Fitvids
		fitvidsInit: function() {
			
			// Fitvids - For responsive videos
			setTimeout( function() {
				$( '.entry-content' ).fitVids();
			}, 500 );
		
		}

	}

	/** Document Ready */
	$( document ).ready( function() {

		// Menu
		match.menuInit();

		// Fitvids - For responsive videos
		match.fitvidsInit();

	} );
	
} )( jQuery );