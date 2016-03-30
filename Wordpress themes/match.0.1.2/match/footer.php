<?php
/**
 * The template for displaying the footer.
 *
 * Contains the closing of the #content div and all content after
 *
 * @package Match
 */
?>

	<footer id="colophon" class="site-footer" role="contentinfo" itemscope="itemscope" itemtype="http://schema.org/WPFooter">
		
		<div class="site-info">
			<div class="container">  

				<div class="row">					
					<div class="col-lg-12">
						<div class="credits">
							<?php do_action( 'match_credits' ); ?>
						</div>
					</div>
				</div>
			
			</div><!-- .container -->
		</div><!-- .site-info -->
		
	</footer><!-- #colophon -->

</div> <!-- #page .site-wrapper -->
<?php wp_footer(); ?>
</body>
</html>