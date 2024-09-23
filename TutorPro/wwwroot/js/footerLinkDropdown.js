function showDropdown(key) {
    // Get the selected button and dropdown
    const selectedButton = document.getElementById(key + 'Button');
    const selectedDropdown = document.getElementById(key + 'Content');
    const items = selectedDropdown.querySelectorAll("a.footer__column__item");
    // Check if the selected dropdown is currently shown
    const isCurrentlyShown = selectedDropdown.classList.contains('show');

    // Hide all dropdowns and remove active class from all buttons
    const dropdowns = document.querySelectorAll('.footer__dropdown-content.show');
    dropdowns.forEach(dropdown => dropdown.classList.remove('show'));

    const buttons = document.querySelectorAll('.footer__dropdown-button.active');
    buttons.forEach(button => button.classList.remove('active'));

    // Toggle the selected dropdown and button based on the previous state
    if (!isCurrentlyShown) {
        selectedDropdown.classList.add('show');
        selectedButton.classList.add('active');
    }
    const culture = selectedButton.getAttribute('data-culture');
    const requests = Array.from(items).map(item => {
        return $.ajax({
            url: `/Umbraco/Api/Materials/IsMaterialPageHasChild`,
            type: 'GET',
            data: { language: key, pageName: item.innerHTML, culture: culture },
            success: function (result) {
                if (!result) {
                    item.classList.add('disable');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
            }
        });
    });
}