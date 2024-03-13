$("#addskills").click(function () {
    
    $("li.list-group-item").next(".error").remove();
    let list = $("#myList");
    let value = $("#skills").val();

    let li = $("<li>", {
        "class": "list-group-item",
        "id": value
    });

    let div1 = $("<div>", {
        "class": "row"
    })

    let div2 = $("<div>", {
        "text": value,
        "class": "col-10"
    })

    let newButton = $("<button>", {
        "text": "X",
        "type": "button",
        "id": value,
        "value": value,
        "class": "btn col-2 p-0 m-0 float-right"
    }).on('click', function () {

        removeSkills(this.value);
    });
    skillsArray.push(value);
    div1.append(div2);
    div1.append(newButton);
    li.append(div1);
    list.append(li);
    var hiddenInput = document.getElementById('SkillNames')
    hiddenInput.value = skillsArray
    $('#skills').val("");
});