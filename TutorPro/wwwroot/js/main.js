$(function () {  

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
    // movingElementsHeight();

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

/*   Parallax elements moving*/
   let parallaxElements = document.querySelectorAll('.parallax-element img');
   for (let i = 0; i < parallaxElements.length; i++){
     let speed = parallaxElements[i].parentNode.getAttribute('data-speed'),
         direction = parallaxElements[i].parentNode.getAttribute('data-direction');
     window.addEventListener('mousemove', function(e) { 
       let x = e.clientX / window.innerWidth;
       let y = e.clientY / window.innerHeight; 
       if(direction == 'true') {
         parallaxElements[i].style.transform = 'translate(' + x * speed + 'px, ' + y * speed + 'px)';
       } else {
           parallaxElements[i].style.transform = 'translate(-' + x * speed + 'px, -' + y * speed + 'px)';
         }
     });    
   }

  // Mask for input[type="tel"]
  $('.request-form input[type="tel"]').each(function(){
    $(this).mask("+999-99-999-99-99",{placeholder:"x"},{autoclear: false});
  }); 

  $('.request-form input.required').on('focus', function(){
    $(this).removeClass('error').parent().removeClass('error');
    $(this).closest('form').find('.request-form__error span').html('');
  }); 

    let validateInputs = function (input) {
        let inputField = $(input),
            type = inputField.attr('type'),
            value = inputField.val(),
            letterNumber = 0;
        switch (type) {
            case 'text':
                for (let i = 0; i < value.length; i++) {
                    if (value[i] != ' ') {
                        letterNumber++;
                    }
                }
                if (value == '') {
                    inputField.addClass('error').parent().addClass('error');
                } else
                    if (letterNumber == 0) {
                        inputField.addClass('error').parent().addClass('error');
                    } else {
                        inputField.removeClass('error').parent().removeClass('error');
                    }
                break;
            case 'email':
                let rv_email = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
                if (value == '') {
                    inputField.addClass('error').parent().addClass('error');
                } else
                    if (rv_email.test(value)) {
                        inputField.removeClass('error').parent().removeClass('error');
                    } else {
                        inputField.addClass('error').parent().addClass('error');
                    }
                break;
            case 'tel':
                for (let i = 0; i < value.length; i++) {
                    if (value[i] == 'x') {
                        letterNumber++;
                    }
                }
                if (value == '') {
                    inputField.addClass('error').parent().addClass('error');
                } else
                    if (letterNumber > 0) {
                        inputField.addClass('error').parent().addClass('error');
                    } else {
                        inputField.removeClass('error').parent().removeClass('error');
                    }
                break;
        }
    };

  $('.request-form').on('submit',function(e){
    e.preventDefault();
    let currentForm = $(this),
        inputs = currentForm.find('input.required'),
        formData = {};

    inputs.each(function(){
      validateInputs($(this));
    });

      inputs.each(function () {
          formData[$(this).attr('name')] = $(this).val();
      });

      // Collect data from hidden fields
      currentForm.find('input[name="AdditionalEmail"]').each(function () {
          if (!formData["AdditionalEmail"]) {
              formData["AdditionalEmail"] = [];
          }
          formData["AdditionalEmail"].push($(this).val());
      });

      formData["SenderMessage"] = currentForm.find('.request-form__message').val();

      var jsonData = JSON.stringify(formData);

    let errorInputs = currentForm.find('input.error');
    if(errorInputs.length === 0) {
        var actionUrl = $('#urlToSend').val();
        $.ajax({
            type: 'POST',
            url: actionUrl,
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                currentForm.closest('.actions__form-wrap').addClass('send');
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
            }
        });
      } else {
        let errorString = currentForm.data('word1'),
            wordDevider1 = ', ',
            wordDevider2 = currentForm.data('word2'),
            word3 = currentForm.data('word3'),
            word4 = currentForm.data('word4');

        if (errorInputs.length === 1) {
            // Only one invalid input
            errorString = word3 + errorInputs.data('name') + word4;
            currentForm.find('.request-form__error span').html(errorString);
        } else {
            // All inputs are invalid
            errorInputs.each(function (index) {
                if (index === (errorInputs.length - 2)) {
                    errorString += $(this).data('name') + wordDevider2;
                } else if (index === (errorInputs.length - 1)) {
                    errorString += $(this).data('name');
                } else {
                    errorString += $(this).data('name') + wordDevider1;
                }
            });
            currentForm.find('.request-form__error span').html(errorString);
        }
      }    
  });

  // Moving elements
  if($('.animated').length != 0) {
    $('.animated__row-wrap').each(function(){
      let duration = $(this).data('duration'),
          leftOffset = $(this).data('left-offset'),
          animatedRows = $(this).find('.animated__row');

      $(this).css({'margin-left': leftOffset});
      animatedRows.each(function(){
        $(this).css({'animation-duration': duration});
      });
    });
  }

   /*Custom Select in section Hero-L2, block "Employee Benefits"*/
	$('.select').each(function() {
    const _this = $(this),
        selectOption = _this.find('option'),
        selectOptionLength = selectOption.length,
        selectedOption = selectOption.filter(':selected'),
        duration = 300;

    _this.hide();
    _this.wrap('<div class="select"></div>');
    $('<div>', {
        class: 'new-select',
        text: _this.data('placeholder')
    }).insertAfter(_this);

    const selectHead = _this.next('.new-select');
    selectHead.addClass('empty');
    $('<div>', {
        class: 'new-select__list'
    }).insertAfter(selectHead);

    const selectList = selectHead.next('.new-select__list');
    for (let i = 0; i < selectOptionLength; i++) {
        $('<div>', {
            class: 'new-select__item',
            html: $('<span>', {
                text: selectOption.eq(i).text()
            })
        })
        .attr('data-value', selectOption.eq(i).val())
        .appendTo(selectList);
    }

    const selectItem = selectList.find('.new-select__item');
    selectList.slideUp(0);
    selectHead.on('click', function() {      
        if ( !$(this).hasClass('on') ) {
          if($(this).hasClass('empty')) {
            $(this).css({'color':'transparent'});
          }
					$('.new-select.on').removeClass('on').css({'color':'#FF6B00'}).next('.new-select__list').slideUp();
            $(this).addClass('on');
            selectList.slideDown(duration);

            selectItem.on('click', function() {
              selectHead.removeClass('empty').css({'color':'#FF6B00'});
              $(this).siblings('.selected').removeClass('selected');
              $(this).addClass('selected');
                let chooseItem = $(this).data('value');

                $('select').val(chooseItem).attr('selected', 'selected');
                selectHead.text( $(this).find('span').text() );

                selectList.slideUp(duration);
                selectHead.removeClass('on');
            });

        } else {
            $(this).removeClass('on');
            selectList.slideUp(duration);
            if($(this).hasClass('empty')) {
              $(this).css({'color':'#FF6B00'});
            }
        }
    });
    $('.new-select__item').on('click', function () {
        const category = $(this).closest(".new-select__list").siblings(".select").attr("id")
        const data = $(this).find('span').html()
        sendRequest(category, data);
    });
  });
});

