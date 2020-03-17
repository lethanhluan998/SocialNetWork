var ProfileController = function () {
    this.initialize = function () {
        registerEvents();
    }
    function registerEvents() {
        //Init validation
        
        $("#btn-addfriend").on('click', function () {
            relationship();
        });
    }
    function relationship() {
        $.ajax({
            type: "POST",
            data: {
                id: $('#val-addfriend').val(),
                action: $('#val-action').val()
            },
            url: '/Profile/RelationShip',
            dataType: "json",

            success: function (response) {
                console.log(response);
                var data = response;
                if (data == Object) {
                    
                } else if (data == 0) {
                    $('#status').html("Đã gửi lời mời kết bạn");
                    $('#val-action').val('CancelRequest');
                }
                else if (data == 1) {
                    $('#status').html("Bạn bè");
                    $('#val-action').val('UnFriend');
                }

            },
            error: function (error) {
                console.log(error);
                $('#status').html("Kết bạn");
                $('#val-action').val('AddFriend');
            }
        });
    }
    
}
