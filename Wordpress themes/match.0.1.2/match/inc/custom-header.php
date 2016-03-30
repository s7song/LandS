<?php
/**
 * Setup the WordPress core custom header feature.
 *
 * @uses match_header_style()
 * @uses match_admin_header_style()
 * @uses match_admin_header_image()
 *
 * @package Match
 */
function match_custom_header_setup() {
	add_theme_support( 'custom-header', apply_filters( 'match_custom_header_args', array(
		'default-image'          => '%s/images/header-default.jpg',
		'random-default'         => false,
		'width'                  => 1920,
		'height'                 => 750,
		'flex-height'            => true,
		'header-text'            => false,
		'wp-head-callback'       => 'match_header_style',
		'admin-head-callback'    => 'match_admin_header_style',
		'admin-preview-callback' => 'match_admin_header_image'
	) ) );
}
add_action( 'after_setup_theme', 'match_custom_header_setup' );

/** Default custom headers packaged with the theme. %s is a placeholder for the theme template directory URI. */
register_default_headers( array(
	
	'match' => array(
		'url'           => '%s/images/header-default.jpg',
		'thumbnail_url' => '%s/images/header-default-thumb.jpg',
		'description'   => __( 'Match', 'match' )
	)

) );

if ( ! function_exists( 'match_header_style' ) ) :
/**
 * Styles the header image and text displayed on the blog
 *
 * @see match_custom_header_setup().
 */
function match_header_style() {
}
endif; // match_header_style

if ( ! function_exists( 'match_admin_header_style' ) ) :
/**
 * Styles the header image displayed on the Appearance > Header admin panel.
 *
 * @see match_custom_header_setup().
 */
function match_admin_header_style() {
?>
	<style type="text/css">
		.appearance_page_custom-header #headimg {
			overflow: hidden;
			border: none;
		}
		#headimg img {
			max-width: 100%;
			height: auto;
		}
	</style>
<?php
}
endif; // match_admin_header_style

if ( ! function_exists( 'match_admin_header_image' ) ) :
/**
 * Custom header image markup displayed on the Appearance > Header admin panel.
 *
 * @see match_custom_header_setup().
 */
function match_admin_header_image() {
?>
	<div id="headimg">
		<?php if ( get_header_image() ) : ?>
		<img src="<?php header_image(); ?>" alt="">
		<?php endif; ?>
	</div>
<?php
}
endif; // match_admin_header_image