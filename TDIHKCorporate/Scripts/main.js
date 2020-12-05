window.addEventListener('load', function () {
	if (document.getElementById('today')) {
		document.getElementById('today').innerHTML = moment().format("dddd, DD MMMM");
	}
});