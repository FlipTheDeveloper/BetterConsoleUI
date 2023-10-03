const textElement = document.getElementById('text');
const cursorElement = document.getElementById('cursor');

const textArray = [
    "Thank you for visiting my website.",
    "View my portfolio.",
    "Let's collaborate.",
    "Hire me.",
    "Connect with me.",
    "Explore my work.",
];
const typingSpeed = 100; // Typing speed in milliseconds
const pauseDuration = 2000; // Pause duration in milliseconds

let arrayIndex = 0;
let charIndex = 0;

function type() {
    if (charIndex < textArray[arrayIndex].length) {
        textElement.textContent += textArray[arrayIndex].charAt(charIndex);
        charIndex++;
        setTimeout(type,
            typingSpeed);
    } else {
        setTimeout(erase, pauseDuration);
    }
}

function erase() {
    if (charIndex > 0) {
        textElement.textContent = textArray[arrayIndex].substring(0, charIndex - 1);
        charIndex--;
        setTimeout(erase, typingSpeed / 2);
    } else {
        arrayIndex = (arrayIndex + 1) % textArray.length;
        setTimeout(type, typingSpeed / 2);
    }
}

type(); // Start the typing effect