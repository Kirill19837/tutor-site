const cookieConsent = document.getElementById('cookies');
const acceptButton = document.querySelector('.cookies__accept');

// Function to set a cookie
function setCookie(name, value, days) {
    let expires = "";
    if (days) {
        const date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + value + expires + "; SameSite=Lax; Secure; path=/";
}

// Function to check if cookie exists
function getCookie(name) {
    const nameEQ = name + "=";
    const cookies = document.cookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
        let cookie = cookies[i];
        while (cookie.charAt(0) == ' ') cookie = cookie.substring(1, cookie.length);
        if (cookie.indexOf(nameEQ) === 0) return cookie.substring(nameEQ.length, cookie.length);
    }
    return null;
}

if (localStorage.getItem("displayed")) {
    if (!getCookie("cookieConsent")) {
        localStorage.removeItem("displayed");
    }
    else {
        cookies.classList.add("accepted");
    }
}

// Handle user click on Accept button
acceptButton.addEventListener('click', () => {
    cookieConsent.classList.add("accepted");
    setCookie('cookieConsent', 'accepted', 365);
    // Hide the banner
    localStorage.setItem("displayed", "none");
});