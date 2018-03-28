jQuery(function(){
	jQuery('.modal').modal();
	jQuery('select').material_select();
	jQuery(".notifications-toggle").sideNav({
		menuWidth: 320,
		edge: 'right'
	});
	jQuery('.notifications-close').on('click', function(){
		jQuery(this).sideNav('hide');
	});
	jQuery(".notifications-toggle").on('click', function(){
		var infoClass = 'info';
		var opener = jQuery(this);
		var parent = opener.closest('li');
		var panelItems = jQuery('#' + opener.attr('data-activates')).find('.notifications-list li');
		var timer = setTimeout(removeItemClass, 5000);

		function removeItemClass(){
			if(parent.hasClass(infoClass)){
				parent.removeClass(infoClass);
			}

			panelItems.each(function(){
				jQuery(this).removeClass('active');
			});
		}

	});
});