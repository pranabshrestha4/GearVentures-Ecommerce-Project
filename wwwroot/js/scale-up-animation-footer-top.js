document.addEventListener('DOMContentLoaded', function () {
    const textElements = document.querySelectorAll('.the-goal-of, .location, .mail, .phone-number, .contact-us, .email-box1, .message-box, .message-button, .phone-icon, .mail-icon, .map-icon, .company-logo-icon1');

    function addAnimationClass() {
        textElements.forEach(element => {
            element.classList.add('animate');
        });
    }

    function onScroll() {
        const elementPosition = textElements[0].getBoundingClientRect().top;

        if (elementPosition < window.innerHeight) {
            addAnimationClass();
            window.removeEventListener('scroll', onScroll); 
        }
    }

    window.addEventListener('scroll', onScroll);
});
