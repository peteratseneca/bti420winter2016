
// This function is called by each button
// The url is important

function fetchData(url) {

    // Get a reference to the DOM element
    var s = document.querySelector('#status');

    // create an xhr object
    var xhr = new XMLHttpRequest();

    // configure its handler
    xhr.onreadystatechange = function () {

        if (xhr.readyState === 4) {
            // request-response cycle has been completed, so continue

            if (xhr.status === 200) {
                // request was successfully completed, and data was received, so continue

                // update the user interface
                s.innerHTML = '';

                // If you're interested in seeing the returned JSON...
                // Open the browser developer tools, and look in the JavaScript console
                console.log(xhr.responseText);

                // Call the appropriate UI-building method

                if (url.endsWith('artists')) {
                    writeArtistList(JSON.parse(xhr.responseText));
                }

                if (url.endsWith('albums')) {
                    writeAlbumList(JSON.parse(xhr.responseText));
                }

                if (url.endsWith('tracks')) {
                    writeTrackList(JSON.parse(xhr.responseText));
                }

            } else {
                // Get a reference to the DOM element
                var e = document.querySelector('#content');
                e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + ")</p>";
                s.innerHTML = '';
            }
        } else {
            s.innerHTML = 'Loading...';
        }
    }

    // configure the xhr object to fetch content
    xhr.open('get', url, true);
    // set the request header
    xhr.setRequestHeader('accept', 'application/json');
    // fetch the content
    xhr.send(null);
}

function writeArtistList(results) {

    // Begin table
    var table = '<table class="table"><tr><th>Artist identifier</th><th>Artist name</th></tr>';

    for (var i = 0; i < results.length; i++) {

        table += '<tr><td>' + results[i].ArtistId + '</td><td>' + results[i].Name + '</td></tr>';
    }

    // End table
    table += '</table>';

    // Get a reference to the DOM element
    var e = document.querySelector('#content');
    e.innerHTML = table;
}

function writeAlbumList(results) {

    // Begin table
    var table = '<table class="table"><tr><th>Album identifier</th><th>Album title</th><th>Artist name</th></tr>';

    for (var i = 0; i < results.length; i++) {

        table += '<tr><td>' + results[i].AlbumId + '</td><td>' + results[i].Title + '</td><td>' + results[i].ArtistName + '</td></tr>';
    }

    // End table
    table += '</table>';

    // Get a reference to the DOM element
    var e = document.querySelector('#content');
    e.innerHTML = table;
}

function writeTrackList(results) {

    // Begin table
    var table = '<table class="table"><tr><th>Track identifier</th><th>Track name</th><th>Album title</th><th>Artist name</th><th>Genre</th><th>Media type</th><th>Length (minutes)</th><th>Unit price</th></tr>';

    for (var i = 0; i < results.length; i++) {

        table += '<tr><td>' + results[i].TrackId + '</td><td>' + results[i].Name + '</td><td>' + results[i].AlbumTitle + '</td><td>' + results[i].AlbumArtistName + '</td><td>' + results[i].GenreName + '</td><td>' + results[i].MediaTypeName + '</td><td>' + results[i].TrackLength + '</td><td>' + results[i].UnitPrice + '</td></tr>';
    }

    // End table
    table += '</table>';

    // Get a reference to the DOM element
    var e = document.querySelector('#content');
    e.innerHTML = table;
}
