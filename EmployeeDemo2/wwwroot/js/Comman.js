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
                    paginationFunction();
                }
            })
        }
    });

}