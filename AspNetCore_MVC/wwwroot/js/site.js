var slider = document.getElementById('slider');
var lightTheme = document.getElementById('light-theme');
var darkTheme = document.getElementById('dark-theme');
var section = document.querySelector('.laptop-slider-section');

function updateImagesAndBackground() {
    var value = ((slider.value - slider.min) / (slider.max - slider.min)) * 100;

    // Justera ClipPath för laptopbilderna så att den mörka bilden visas till vänster
    // och den ljusa bilden till höger
    darkTheme.style.clipPath = `inset(0 ${100 - value}% 0 0)`; // Mörk bild till vänster
    lightTheme.style.clipPath = `inset(0 0 0 ${value}%)`; // Ljus bild till höger

    // Uppdatera bakgrundsfärgen för hela sektionen
    section.style.background = `linear-gradient(to right, black ${value}%, white ${value}%)`;
}
slider.value = 50; 
updateImagesAndBackground();

slider.addEventListener('input', function () {
    requestAnimationFrame(updateImagesAndBackground);
});