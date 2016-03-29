// Show and hide tracks by genre name
function toggleGenre(genreId, selected) {

    // Fetch matching elements
    // Looking for div with a class name that matches the genre name
    var tracks = document.querySelectorAll('div.' + genreId);

    if (selected) {

        for (var i = 0; i < tracks.length; i++) {

            tracks[i].style.display = 'block';
        }

    } else {

        for (var i = 0; i < tracks.length; i++) {

            tracks[i].style.display = 'none';
        }
    }
}