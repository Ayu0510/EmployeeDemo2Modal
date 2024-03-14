function saveFormAlert(e)
{
    e.preventDefault();
    Swal.fire({
        title: "Do you want to save the changes?",
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Save",
        denyButtonText: `Don't save`
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            debugger
            let myForm = document.getElementById("myForm");
            let formsubmit = myForm.submit();
            if (e.result = true) {
                Swal.fire("Saved!", "", "success")
            }
        } else if (result.isDenied) {
            Swal.fire("Changes are not saved", "", "info");
        }
        else {
            history.back();
        }
    });
}

function deleteEmployeeAlert(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    debugger
                    pagination();
                }
            })
        }
    });

}

$(document).ready(function () {

    


    $(".fold-table .view #view").on("click", function () {
        debugger
        $(this).toggleClass("open").next(".fold").toggleClass("open");
    });

        var placeHoderElement = $('#PlaceHolder');
        $('.upsert').click( function () {
            var url = $(this).data('url');
            var decodedUrl = decodeURIComponent(url);
            $.get(url).done(function (data) {
                placeHoderElement.html(data);
                placeHoderElement.find('.modal').modal('show');
            })
        })
        placeHoderElement.on('click', '[data-save="modal"]', function (event) {

            let IsValid = Validate("Add-employee");
            if (IsValid == true) {
                var form = $(this).parents(".modal").find('#myForm');
                var actionurl = $('#myForm').attr('action')
                var sendData = new FormData(form[0]);

                $.ajax({
                    url: actionurl,
                    type: "POST",
                    data: sendData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        placeHoderElement.find(".modal").modal("hide");
                    }
                })
                loaddata();
            }
            else {
                placeHoderElement.find(".modal").modal("show");
            }
        })
})




