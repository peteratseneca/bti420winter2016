function previewImage(event) {
    // Get a reference to the image element
    var div = document.querySelector("#photoPreview");
    // Append an img element
    div.innerHTML = '<p><img src="' + URL.createObjectURL(event.target.files[0]) + '" alt="" width="280"></p>'
};