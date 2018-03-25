$(document).ready(function () {

    var urls = {
        add: "/Synonym/Add",
        getAll: "/Synonym/GetAll"
    };

    var populateSynonyms = function () {
        $.ajax({
            type: "get",
            cache: false,
            url: urls.getAll,
            success: function (result) {
                if (result.items) {
                    var renderedItems = renderItems(result.items);
                    $("#synonymsList").html(renderedItems);
                }
            }

        });

    };

    var renderItems = function (items) {
        var result = "";
        for (var i = 0; i < items.length; i++) {
            var synonyms = renderSynonyms(items[i].Synonyms);
            result += "<li class='list-group-item'>" + items[i].Term + synonyms + "</li>";
        }
               
        return result;
    };

    var renderSynonyms = function (synonyms) {
        var result = "<div class='synonyms'>";
        for (var i = 0; i < synonyms.length; i++) {
            result += "<span class='badge'>" + synonyms[i] +"</span>";
        }
        result += "</div>";
        return result;
    };

    populateSynonyms();

    $("#synonymAddBtn").on("click", function (e) {
        e.preventDefault();

        var data = {
            Term: $("#term").val(),
            Synonyms: $("#synonyms").val()
        };

        $.ajax({
            type: "post",
            url: urls.add,
            data: data,
            success: function (result) {
                if (result.success) {
                    populateSynonyms();
                }
                else {
                    alert(result.message);
                }
            }
        })

    });
    
});