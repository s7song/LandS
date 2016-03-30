<?php
/**
 * @package Match
 */
?>

<article id="post-<?php the_ID(); ?>" <?php post_class(); ?> itemscope="itemscope" itemtype="http://schema.org/BlogPosting" itemprop="blogPost">

	<div class="entry-media">
		<?php match_post_thumbnail(); ?>
		<div class="entry-format">
			<a href="<?php echo esc_url( get_post_format_link( 'quote' ) ); ?>" title="<?php echo esc_attr( __( 'All Quote Posts', 'match' ) ); ?>" class="entry-format-icon">
				<i class="fa fa-quote-left"></i>
			</a>
		</div><!-- .entry-format -->
	</div><!-- .entry-media -->

	<header class="entry-header">
		<?php
		if ( is_single() ) :
			the_title( '<h1 class="entry-title">', '</h1>' );
		else :
			the_title( '<h1 class="entry-title"><a href="' . esc_url( get_permalink() ) . '" rel="bookmark">', '</a></h1>' );
		endif;
		?>

		<div class="entry-meta">
			<?php match_posted_on(); ?>
		</div><!-- .entry-meta -->	
	</header><!-- .entry-header -->

	<div class="entry-content">
		<?php the_content( __( 'Continue reading <span class="meta-nav">&rarr;</span>', 'match' ) ); ?>
		<?php
			wp_link_pages( array(
				'before'      => '<div class="page-links"><span class="page-links-title">' . __( 'Pages:', 'match' ) . '</span>',
				'after'       => '</div>',
				'link_before' => '<span>',
				'link_after'  => '</span>',
			) );
		?>
	</div><!-- .entry-content -->

	<footer class="entry-meta entry-meta-footer">
		<?php
			/* translators: used between list items, there is a space after the comma */
			$categories_list = get_the_category_list( __( ', ', 'match' ) );
			if ( $categories_list && match_categorized_blog() ) :
		?>
		<span class="cat-links">
			<?php printf( __( 'Posted in %1$s', 'match' ), $categories_list ); ?>
		</span>
		<?php endif; // End if categories ?>

		<?php
			/* translators: used between list items, there is a space after the comma */
			$tags_list = get_the_tag_list( '', __( ', ', 'match' ) );
			if ( $tags_list ) :
		?>
		<span class="tags-links">
			<?php printf( __( 'Tagged %1$s', 'match' ), $tags_list ); ?>
		</span>
		<?php endif; // End if $tags_list ?>

		<?php if ( ! post_password_required() && ( comments_open() || '0' != get_comments_number() ) ) : ?>
		<span class="comments-link"><?php comments_popup_link( __( 'Leave a comment', 'match' ), __( '1 Comment', 'match' ), __( '% Comments', 'match' ) ); ?></span>
		<?php endif; ?>

		<?php edit_post_link( __( 'Edit', 'match' ), '<span class="edit-link">', '</span>' ); ?>
	</footer><!-- .entry-meta -->

</article><!-- #post-## -->