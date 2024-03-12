var loadFile = function (event) {
    var output = document.getElementById('output');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
};

$(document).ready(function (){
    $("#image").change(function () {
        if (this.files && this.files[0]) {

            let reader = new FileReader();

            reader.onload = function (e) {
                $('#output').attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);
        }
    });
})

function hideImg() {
    document.getElementById("output")
        .style.display = "none";
    document.getElementById("ImgDetach")
        .style.display = "none";
}
function showImg() {
    document.getElementById("output")
        .style.display = "block";
    document.getElementById("ImgDetach")
        .style.display = "block";
}

function ImgDelete() {
    $('#imgDiv').load(' #imgDiv')
    Swal.fire({
        position: "center",
        icon: "success",
        title: "Image Removed",
        showConfirmButton: false,
        timer: 1500
    });
}