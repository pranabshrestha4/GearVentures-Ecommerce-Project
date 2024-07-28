const slider = document.querySelector('.slider');
const images = document.querySelectorAll('.slider img');

let currentImageIndex = 0;

function nextImage() {
    images[currentImageIndex].style.opacity = 0;
    currentImageIndex = (currentImageIndex + 1) % images.length;
    images[currentImageIndex].style.opacity = 1;
}
setInterval(nextImage, 3000);
