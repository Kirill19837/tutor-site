$(function(){

  // Burger-menu
	$('#header-burger').on('click', function(e) {
		e.preventDefault();
    let navigation = $('#header-navigation'),
        menu = $('#header-menu');
    if($(this).hasClass('active')) {
      $(this).removeClass('active');
      navigation.fadeOut(400).removeClass('open');
      $('body').css('overflow','auto'); 
    } else {
        $(this).addClass('active');   
        navigation.fadeIn(400).addClass('open');
        $('body').css('overflow','hidden');
      }
    
    navigation.on('mouseup', function (e){
      if (!menu.is(e.target) && menu.has(e.target).length === 0) {
        $('#header-burger').removeClass('active');
        navigation.fadeOut(300).removeClass('open');
        $('body').css('overflow','auto');
      }
    });
	});	

  // Language block
  // let currentLangugeFunction = function() {
  //   let currentLanguage = $('.languages__current').data('choice'),
  //       languages = $('.languages__current').siblings('.languages__list').find('.languages__link');
  //   languages.each(function(index,element) {
  //     if($(this).data('lang') === currentLanguage) {
  //       $(this).hide();
  //     }
  //   });
  // };

  // currentLangugeFunction();

  $('.languages__current').on('click', function(e){
    e.preventDefault();
    if(!$(this).hasClass('active')) {
      $(this).addClass('active').siblings('.languages__list').slideDown(300);
    } else {
        $(this).removeClass('active').siblings('.languages__list').slideUp(300);
      }
  });

  $('.languages__link').on('click', function(e){
    e.preventDefault();
    // if(!$(this).hasClass('active')) {
      let lang = $(this).data('lang'),
          text = $(this).html(),
          langList = $(this).parent('.languages__list'),
          langCurrent = langList.siblings('.languages__current');

      $('.languages__link.active').removeClass('active');
      $(this).addClass('active');
      langCurrent.removeClass('active').attr('data-choice',lang).html(text);
    // }
    langList.slideUp(300);
  });

  // $('.header__languages-button').on('click', function(e){
  //   // e.preventDefault();
  //   if(!$(this).hasClass('active')) {
  //     $(this).siblings('.active').removeClass('active');
  //     $(this).addClass('active');
  //   }
  // });
  // console.log($('.languages__link [data-lang="' + currentLanguage +'"]').attr('href'));
  // $('.language__link [data-lang]')

  // Remove action block to mobile menu
  let removeMenuElements = function() {
		if($(document).width() < 577) {
			$('#header-menu').append($('.header__languages'));
			$('#header-menu').append($('.header__link'));
		} else {
      $('#header-actions').append($('.header__languages'));
			$('#header-actions').append($('.header__link'));
    }
	};

  removeMenuElements();
  $(window).on('resize', function(){
		removeMenuElements();

    if($(window).width() > 992) {
      $('#header-burger').removeClass('active');
      $('#header-navigation').fadeOut(400).removeClass('open');
      $('body').css('overflow','auto');
    }
  });  

});