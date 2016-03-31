
// Artist selected
function artistSelected(element) {

    // Set the initial state of the send button
    document.querySelector('#sendButton').disabled = 'disabled';

    // Get a reference to the DOM element
    var e = document.querySelector('#task2');

    // create an xhr object
    var xhr = new XMLHttpRequest();

    // configure its handler
    xhr.onreadystatechange = function () {

        if (xhr.readyState === 4) {
            // request-response cycle has been completed, so continue

            if (xhr.status === 200) {
                // request was successfully completed, and data was received, so continue

                // update the user interface
                e.innerHTML = xhr.responseText;

            } else {
                e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + ")</p>";
            }
        } else {
            //e.innerHTML = "<p>Loading...</p>";
        }
        // show the content
        e.style.display = 'block';
    }

    // configure the xhr object to fetch content
    xhr.open('get', '/tracks/albums/' + element.value, true);
    // fetch the content
    xhr.send(null);

    // Clear the track list div
    var task3 = document.querySelector("#task3");
    task3.innerHTML = "";
}

// Album selected
function albumSelected(element) {

    // Set the initial state of the send button
    document.querySelector('#sendButton').disabled = 'disabled';

    // Get a reference to the DOM element
    var e = document.querySelector('#task3');

    // create an xhr object
    var xhr = new XMLHttpRequest();

    // configure its handler
    xhr.onreadystatechange = function () {

        if (xhr.readyState === 4) {
            // request-response cycle has been completed, so continue

            if (xhr.status === 200) {
                // request was successfully completed, and data was received, so continue

                // update the user interface
                e.innerHTML = xhr.responseText;

                // Enable (activate) the send button
                document.querySelector('#sendButton').disabled = '';

            } else {
                e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + ")</p>";
            }
        } else {
            //e.innerHTML = "<p>Loading...</p>";
        }
        // show the content
        e.style.display = 'block';
    }

    // configure the xhr object to fetch content
    xhr.open('get', '/tracks/tracks/' + element.value, true);
    // fetch the content
    xhr.send(null);
}
