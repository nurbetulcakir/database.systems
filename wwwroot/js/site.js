// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function initMap() {
    var mapCenter = { lat: 41.02687072753906, lng: 28.969064712524414 }; // Coordinates for New York City (you can set your preferred location)
    var map = new google.maps.Map(document.getElementById('map'), {
        center: mapCenter,
        zoom: 15 // Adjust the zoom level as needed
    });

    // Add a marker
    var marker = new google.maps.Marker({
        position: mapCenter,
        map: map,
        title: 'Hello World!'
    });
}