
//navbar toggle
const toggleButton = document.getElementsByClassName('toggle-button')[0]
const navbarLinks = document.getElementsByClassName('navbar-links')[0]
toggleButton.addEventListener('click', () => {
    navbarLinks.classList.toggle('active')
})

//gives theDate to the contact form
var date = new Date();
var day = date.getDate();
var month = date.getMonth() + 1;
var year = date.getFullYear();
if (month < 10) month = "0" + month;
if (day < 10) day = "0" + day;
var today = year + "-" + month + "-" + day;
document.getElementById('startDate').value = today;

//element animations
const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
		console.log(entry)
		if (entry.isIntersecting) {
            entry.target.classList.add('show');
        } else {
            entry.target.classList.remove('show');
        }
    });
});
const hiddenElements = document.querySelectorAll('.hidden');
hiddenElements.forEach((e1) => observer.observe(e1));

//masage phone number form field values using cleave.js
document.addEventListener('DOMContentLoaded', () => {
    const cleave = new Cleave('#Phone', {
        numericOnly: true,
        blocks: [0, 3, 0, 3, 4],
        delimiters: ["(", ")", " ", "-"],
        phoneRegionCode: 'US'
    });
});

//displays contact confirmation
window.alert = function (message) {
    alert(message);
}


