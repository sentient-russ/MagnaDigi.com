//selection animation
const trip1 = document.getElementById("detector1");
const trip2 = document.getElementById("detector2");
const trip3 = document.getElementById("detector3");
const trip4 = document.getElementById("detector4");

const signal1 = document.getElementById("indicator1");
const signal2 = document.getElementById("indicator2");
const signal3 = document.getElementById("indicator3");
const signal4 = document.getElementById("indicator4");

trip1.addEventListener("mouseover", () => {
	signal1.style.opacity = '.5';
});
trip1.addEventListener("mouseout", () => {
	signal1.style.opacity = '0';
});
trip2.addEventListener("mouseover", () => {
	signal2.style.opacity = '.5';
});
trip2.addEventListener("mouseout", () => {
	signal2.style.opacity = '0';
});
trip3.addEventListener("mouseover", () => {
	signal3.style.opacity = '.5';
});
trip3.addEventListener("mouseout", () => {
	signal3.style.opacity = '0';
});
trip4.addEventListener("mouseover", () => {
	signal4.style.opacity = '.5';
});
trip4.addEventListener("mouseout", () => {
	signal4.style.opacity = '0';
});
//with on hover animation
/*const actionCallDiv = document.getElementById("tact");
const magnaDiv = document.getElementById("magnadigi-text");
const withDiv = document.getElementById("with-text");

actionCallDiv.addEventListener("mouseover", () => {
	magnaDiv.style.color = '#333';
	magnaDiv.style.opacity = '.5';
	withDiv.style.color = '#333';
	withDiv.style.opacity = '.5';

});
actionCallDiv.addEventListener("mouseout", () => {
	magnaDiv.style.color = '#333';
	magnaDiv.style.opacity = '1';
	withDiv.style.color = '#333';
	withDiv.style.opacity = '1';
});*/

const outputDiv = document.getElementById('magic-typing-container');
const txt1 = `software();       `;
const txt2 = `greatness();      `;
const txt3 = `success();      `;
const txt4 = `happyDance();      `;
const txt5 = `confetti();      `;

var displayString = txt1;

let i = 0;
const intervalId = setInterval(function() {
	outputDiv.innerHTML += displayString[i];

	i++;
	if (i === displayString.length + 1) {
		i = 0;
		outputDiv.innerHTML = '<span class="before-text">magnadigi.create_</span>';

		if (displayString == txt1) {
			displayString = txt2;
		} else if (displayString == txt2) {
			displayString = txt3;
		} else {
			displayString = txt1;
        }

	}
},150);

//contact popup
$('#contactDialog').popup();
