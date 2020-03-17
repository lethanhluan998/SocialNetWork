var HomeController = function () {
    this.initialize = function () {
        registerEvents();
    }
    var i=0;
    function registerEvents() {
        //Init validation
        $(".open-message").on('click', function () {
            var username = $(this).attr("value");
            var fullName = $(this).html();
            console.log(username);
            console.log(fullName);
            openMessage(username, fullName, i);
            i++;
        });
        

    }
    function openMessage(username, fullName,x) {
        //var username = $('.open-message').attr("value");
        
        $.ajax({
            type: "GET",
            url: '/Chat/ActionMessage',
            data: {
                userName: username
            },
            dataType: "JSON",
            success: function (response) {

                
                console.log(response);
                var data = response;
                console.log(data.RoomChatViewModel.Messages[0].FullName);
                console.log("rom thu "+x);
                let tmpl = $('#my-template').html();
                Mustache.parse(tmpl);
                let rendered = Mustache.render(tmpl,
                    {
                        RoomId: data.RoomChatViewModel.Id,
                        nameRomChat: fullName
                    }
                );
                $("#popup-chat").append(rendered);
                $(".popup-chat__window").addClass("show-me");
                $(".popup-chat__window").addClass("user-is-active"); 
                console.log("Dưới đây là data");

                
                for (var i = 0; i < data.RoomChatViewModel.Messages.length; i++) {
                    var text = $(document.createTextNode(data.RoomChatViewModel.Messages[i].Message_text));
                    var divText = $(document.createElement("div"));
                    divText.addClass("text");
                    divText.append(text);
                    var divMessageBody = $(document.createElement("div"));
                    divMessageBody.addClass("message__body");
                    divMessageBody.append(divText);
                    
                        if (data.RoomChatViewModel.Messages[i].FullName === data.MyName) {
                            var divSentMessage = $(document.createElement("div"));
                            divSentMessage.addClass("sent-message mb-4");
                            divSentMessage.append(divMessageBody);
                            $("#" + data.RoomChatViewModel.Id).append(divSentMessage);
                            
                        } else {
                            var divReceived = $(document.createElement("div"));
                            divReceived.addClass("received-message mb-4");
                            divReceived.append(divMessageBody);
                            $("#" + data.RoomChatViewModel.Id).append(divReceived);
                            
                        }
                    
             //       $(".add-mess-list").append(divMessagesList);
                }
                
                $(".send-message-input-1").each(function () {
                    $(this).keyup(function (event) {
                        if (event.keyCode === 13) {
                            
                            var userName = $('#userInput1').val();
                            var message = $(this).val();
                            var idRoom = $(this).data('id');
                            $("#" + idRoom).prop('disabled', false);
                            console.log(userName + " and " + message + " and " + idRoom);
                            $.ajax({
                                type: "POST",
                                url: '/Chat/LoadMessage',
                                data: {
                                    userName: userName,
                                    message: message,
                                    idRoom: idRoom
                                },
                                dataType: "JSON",
                                success: function (response) {

                                }
                            })
                        }
                    });
                });
            },
            error: function (error) {
                console.log(error);
                $('#status').html("Kết bạn");
                $('#val-action').val('AddFriend');
            }
        })
            .done(function (response) {
                console.log("asdfasdfasd");
                var data = response;
                if (data == Object) {
                    console.log("vao dc");
                } 
            });
    }
}
