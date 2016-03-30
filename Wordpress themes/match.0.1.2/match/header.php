<?php
/**
 * The Header for our theme.
 *
 * Displays all of the <head> section and everything
 *
 * @package Match
 */
?><!DOCTYPE html>
<!--[if lt IE 7]> <html class="no-js lt-ie9 lt-ie8 lt-ie7" <?php language_attributes(); ?>> <![endif]-->
<!--[if IE 7]> <html class="no-js lt-ie9 lt-ie8" <?php language_attributes(); ?>> <![endif]-->
<!--[if IE 8]> <html class="no-js lt-ie9" <?php language_attributes(); ?>> <![endif]-->
<!--[if gt IE 8]><!--> <html class="no-js" <?php language_attributes(); ?>> <!--<![endif]-->
<head>
<meta charset="<?php bloginfo( 'charset' ); ?>">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<link rel="profile" href="http://gmpg.org/xfn/11">
<link rel="pingback" href="<?php bloginfo( 'pingback_url' ); ?>" />

<?php wp_head(); ?>
</head>
<body <?php body_class(); ?> itemscope="itemscope" itemtype="http://schema.org/WebPage">
<div id="page" class="site-wrapper hfeed site">
	<?php do_action( 'before' ); ?>
	<header id="masthead" class="site-header" role="banner" itemscope="itemscope" itemtype="http://schema.org/WPHeader">

		<div class="sitebar">
			<div class="container">
				<div class="sitebar-inside">

					<div class="site-branding">
						<h2 class="site-title" itemprop="headline"><a href="<?php echo esc_url( home_url( '/' ) ); ?>" title="<?php echo esc_attr( get_bloginfo( 'name', 'display' ) ); ?>" rel="home"><?php bloginfo( 'name' ); ?></a></h2>
						<h3 class="site-description" item	prop="description"><?php bloginfo( 'description' ); ?></h3>
					</div>

					<nav id="site-navigation" class="main-navigation" role="navigation" itemscope="itemscope" itemtype="http://schema.org/SiteNavigationElement">
						<div class="menu-toggle-wrapper">
							<a href="#" tabindex="0" class="fa fa-bars fa-2x slicknav-btn slicknav-collapsed"><span class="slicknav-btn-text">Menu</span></a>
						</div>
						<a class="skip-link screen-reader-text" href="#content"><?php _e( 'Skip to content', 'match' ); ?></a>

						<?php
						wp_nav_menu( apply_filters( 'match_wp_nav_menu_args', array(
							'container' => 'div',
							'container_class' => 'site-primary-menu',
							'theme_location' => 'primary',
							'menu_class' => 'primary-menu sf-menu'
						) ) );
						?>
					</nav>

				</div>
			</div>
		</div> <!-- .sitebar -->

		<?php if ( get_header_image() ): ?>
		<div class="header-custom">
			<img src="<?php esc_url( header_image() ); ?>" class="img-responsive" alt="" />
		</div>
		<?php endif; ?>

	</header> <!-- #masthead -->