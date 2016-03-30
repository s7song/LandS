<?php
/**
 * The Template for displaying all single posts.
 *
 * @package Match
 */

get_header(); ?>

<div id="content" class="site-content">

	<div class="container">
		<div class="row">

			<div id="primary" class="content-area col-xs-12 col-sm-12 col-md-12 col-lg-8">
				<main id="main" class="site-main" role="main" itemprop="mainContentOfPage" itemscope="itemscope" itemtype="http://schema.org/Blog">	  
				  
				<?php while ( have_posts() ) : the_post(); ?>

					<?php
						/*
						* Are we using a post format? If so, use the content-someformat.php template.
						* If not, use the content-single.php template.
						*/
						$format = ( '' != get_post_format() ) ? get_post_format() : 'single';
						get_template_part( 'content', $format );
					?>

					<?php match_post_nav(); ?>

					<?php
						// If comments are open or we have at least one comment, load up the comment template
						if ( comments_open() || '0' != get_comments_number() ) :
							comments_template();
						endif;
					?>

				<?php endwhile; // end of the loop. ?>

				</main><!-- #main -->
			</div><!-- #primary -->

			<?php get_sidebar(); ?>

		</div><!-- .row -->
	</div><!-- .container -->

</div><!-- #content -->

<?php get_footer(); ?>