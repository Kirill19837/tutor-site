function showDropdown(key) {
    // Get the selected button and dropdown
    const selectedButton = document.getElementById(key + 'Button');
    const selectedDropdown = document.getElementById(key + 'Content');

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
}