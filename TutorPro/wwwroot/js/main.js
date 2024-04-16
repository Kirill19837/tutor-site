$(function () {

  // $('.str_static').removeClass('str_static');

  if($('.moving-element').length != 0) {
    $('.moving-element').each(function(){
      $(this).liMarquee({
        direction: 'left', //Указывает направление движения содержимого контейнера (left | right | up | down)
        loop: -1, //Задает, сколько раз будет прокручиваться содержимое. "-1" для бесконечного воспроизведения движения
        scrolldelay: 0, //Величина задержки в миллисекундах между движениями
        scrollamount: 50, //Скорость движения контента (px/sec)
        circular: true, //Если "true" - строка непрерывная 
        drag: true, //Если "true" - включено перетаскивание строки
        runshort: false, //Если "true" - короткая строка тоже "бегает", "false" - стоит на месте
        hoverstop: true, //true - строка останавливается при наведении курсора мыши, false - строка не останавливается
        inverthover: false, //false - стандартное поведение. Если "true" - строка начинает движение только при наведении курсора
      });
    });
  }

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
      let lang = $(this).data('lang'),
          text = $(this).html(),
          langList = $(this).parent('.languages__list'),
          langCurrent = langList.siblings('.languages__current');

      $('.languages__link.active').removeClass('active');
      $(this).addClass('active');
      langCurrent.removeClass('active').attr('data-choice',lang).html(text);
    langList.slideUp(300);
  });

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

  // Cookies
  $('.cookies__accept').on('click', function(e){
      e.preventDefault();
      $(this).closest('.cookies').fadeOut(300).addClass('accepted');
  });

  // Mask for input[type="tel"]
  $('.request-form input[type="tel"]').each(function(){
    $(this).mask("+380-99-999-99-99",{placeholder:"x"},{autoclear: false});
  }); 

  $('.request-form input.required').on('focus', function(){
    $(this).removeClass('error').parent().removeClass('error');
    $(this).closest('form').find('.request-form__error span').html('');
  }); 

  let validateInputs = function(input) {
		let inputField = $(input),
        type = inputField.attr('type'),
        value = inputField.val(),
        letterNumber = 0;      
		switch(type)
		{
			case 'text':	
        for(let i=0; i<value.length; i++) {
          if(value[i] != ' ') {
            letterNumber++;
          }
        }			
				if(value == '') {
					inputField.addClass('error').parent().addClass('error');
				} else
					if(letterNumber == 0) {
						inputField.addClass('error').parent().addClass('error');
					} else { 
							inputField.removeClass('error').parent().removeClass('error');
						}
			break;
      case 'email':
				let rv_email = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
				if(value == '') {
					inputField.addClass('error').parent().addClass('error');
				} else
					if(rv_email.test(value)) {
						inputField.removeClass('error').parent().removeClass('error');
					}	else {
            inputField.addClass('error').parent().addClass('error');
						}
			break;
      case 'tel':
        for(let i=0; i<value.length; i++) {
          if(value[i] == 'x') {
            letterNumber++;
          }
        }
				if(value == '') {
					inputField.addClass('error').parent().addClass('error');
				} else
					if(letterNumber > 0) {
            inputField.addClass('error').parent().addClass('error');					
					}	else {
            inputField.removeClass('error').parent().removeClass('error');
						}
			break;
			} 
	};

  $('.request-form').on('submit',function(e){
    e.preventDefault();
    let currentForm = $(this),
        inputs = currentForm.find('input.required');

    inputs.each(function(){
      validateInputs($(this));
    });

    let errorInputs = currentForm.find('input.error');
    if(errorInputs.length === 0) {
      // Обаботка и отправка запроса
      } else {
        let errorString = 'Enter ',
            wordDevider1 = ', ',
            wordDevider2 = ' and ';

        if(errorInputs.length === 1) {
          // Only one invalid input
          errorString = 'Invalid ' + errorInputs.data('name') + ' format';
          currentForm.find('.request-form__error span').html(errorString);
        } else {
          // All inputs are invalid
          errorInputs.each(function(index){
            if(index === (errorInputs.length - 2)) {
              errorString += $(this).data('name') + wordDevider2;
            } else if(index === (errorInputs.length - 1)) {
                errorString += $(this).data('name');
              } else {
                  errorString += $(this).data('name') + wordDevider1;
                }
          });
          currentForm.find('.request-form__error span').html(errorString);
        }
      }    
  });

  // if($('.moving-element').length != 0) {
  //   $('.moving-element').each(function(){
  //     $(this).liMarquee({
  //       direction: 'left', //Указывает направление движения содержимого контейнера (left | right | up | down)
  //       loop: -1, //Задает, сколько раз будет прокручиваться содержимое. "-1" для бесконечного воспроизведения движения
  //       scrolldelay: 0, //Величина задержки в миллисекундах между движениями
  //       scrollamount: 50, //Скорость движения контента (px/sec)
  //       circular: true, //Если "true" - строка непрерывная 
  //       drag: true, //Если "true" - включено перетаскивание строки
  //       runshort: false, //Если "true" - короткая строка тоже "бегает", "false" - стоит на месте
  //       hoverstop: true, //true - строка останавливается при наведении курсора мыши, false - строка не останавливается
  //       inverthover: false, //false - стандартное поведение. Если "true" - строка начинает движение только при наведении курсора
  //     });
  //   });
  // }

});