/*
 * License: GNU General Public License v2 or later
 * http://www.gnu.org/licenses/gpl-2.0.html
 *
 */

( function( $ ) {

	/** Document Ready */
	$( document ).ready( function() {
		
		$( document ).keydown( function( e ) {
			
			var url = false;
			
			if ( e.which == 37 ) {  // Left arrow key code
				url = $( '.previous-image a' ).attr( 'href' );
			}
			
			else if ( e.which == 39 ) {  // Right arrow key code
				url = $( '.entry-attachment a' ).attr( 'href' );
			}
			
			if ( url && ( !$( 'textarea, input' ).is( ':focus' ) ) ) {
				window.location = url;
			}
		
		} );
	
	} );

} )( jQuery );