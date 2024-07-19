document.getElementById('subscriptionForm').addEventListener('submit', function (e) {
    e.preventDefault();

    var email = document.getElementById('email').value;

    var button = document.querySelector('.subscription-form__button');
    var sText = button.getAttribute('data-s_text');
    var culture = button.getAttribute('data-culture');

    var spinner = document.getElementById('loading-spinner');
    var statusP = document.getElementById('status_text');
    var checkBox = document.getElementById('custom-checkbox');
    var input = document.getElementById('subscription-form__item');

    button.style.display = 'none';
    spinner.style.display = 'inline-block';

    $.ajax({
        url: '/Umbraco/Api/Newsletter/Subscribe',
        method: 'POST',
        data: {
            email: email,
            culture: culture,
        },
        success: function (response) {
            button.style.display = 'inline-block';
            spinner.style.display = 'none';

            statusP.textContent = sText;
            checkBox.style.display = 'none';

            input.style.borderColor = "#fdfdfd"
        },
        error: function (xhr, status, error) {
            button.style.display = 'inline-block';
            spinner.style.display = 'none';

            input.style.borderColor = "#ff2323"
        }
    });
});