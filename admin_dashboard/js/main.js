function documentReady(){

  function showNoty(){
    var showNotys = localStorage.getItem('showNoty');
    if(!(showNotys === null) && !(showNotys == '')){
      var showNoty = JSON.parse(localStorage.getItem('showNoty'));
      for(var i = 0; i < showNoty.length; i++){
        var message = showNoty[i].message;
        var type = showNoty[i].type;
        new Noty({
          text: message,
          theme: 'relax',
          modal: true,
          type: type,
          timeout: 3000
        }).show();
      }
      // Now we have shown all the notifications, clear the list of messages to show.
      // This will stop the notifications being shown if the user refreshed the current page
      localStorage.setItem('showNoty', '');
    }
  }

  function getXWords(str, x){
    var spaceCount = (str.split(" ").length - 1);
    if (spaceCount > x-1){
      return str.split(/\s+/).slice(0,x).join(" ") + "...";
    } else {
      return str;
    }
  }

  function Post(id, postId, title, email, body){
    this.id = id;
    this.postId = postId;
    this.name = title;
    this.email = email;
    this.body = body;
  }

  function getNumberOfPosts(){  
    $.ajax({
      url: "http://localhost:3000/comments",
      method: 'GET',
      dataType: 'json'
    }).done(function(data){
      var posts = new Array();
      $.each(data, function(i, post){
        posts.push(new Post(post.id, post.userId, post.title, post.email, post.body));
      })
      var postBadge = $('span.numOfPosts');
      $.each(postBadge, function(i, badge){
        badge.innerHTML = posts.length;
      });

    })
  };

  function Page(id, userId, title, body){
    this.id = id;
    this.userId = userId;
    this.title = title;
    this.body = body;
  }

  function getNumberOfPages(){  
    $.ajax({
      url: "http://localhost:3000/posts",
      method: 'GET',
      dataType: 'json'
    }).done(function(data){
      var pages = new Array();
      $.each(data, function(i, page){
        pages.push(new Page(page.id, page.userId, page.title, page.body));
      })
      var pageBadge = $('span.numOfPages');
      $.each(pageBadge, function(i, badge){
        badge.innerHTML = pages.length;
      });

    })
  };

  function User(id, name, username, email, website){
    this.id = id;
    this.name = name;
    this.username = username;
    this.email = email;
    this.website = website;
  };

  function getNumberOfUsers(){  
    $.ajax({
      url: "http://localhost:3000/users",
      method: 'GET',
      dataType: 'json'
    }).done(function(data){
      var users = new Array();
      $.each(data, function(i, user){
        users.push(new User(user.id, user.name, user.username, user.email, user.website));
      })
      var userBadge = $('span.numOfUsers');
      $.each(userBadge, function(i, badge){
        badge.innerHTML = users.length;
      });
    })
  };

  function getNumberOfVisitors(){
    var visitorBadge = $(".numOfVisitors");
    $.each(visitorBadge, function(i, badge){
      badge.innerHTML = Math.floor(Math.random() * 2000);
    });
  };

  function updateProgressBars(){
    var progressBars = $(".progress-bar");
    $.each(progressBars, function(i, progressbar){
      var random = Math.floor(Math.random() * 100);
      progressbar.innerHTML = random + " %";
      progressbar.style = "width: " + random + "%";
    });
  }

  function updateUsersTable(usersPage){
    var ii = 1;
    $.ajax({
      url: "http://localhost:3000/users",
      method: 'GET',
      dataType: 'json'
    }).done(function(data){
      var table = $('table.usersTable')
      $.each(data, function(i, user){
        // Only show the last 7 users which have been created
        if (ii > data.length - 7 || usersPage){
          var date = moment(user.joined, "DD/MM/YYYY", true).format("DD MMM YYYY"); //Format the joined date in DD MMM YYYY format
          if (usersPage) {
            table[0].innerHTML += "<tr>"+
                                    "<td>"+user.name+"</td>"+
                                    "<td>"+user.email+"</td>"+
                                    "<td>"+date+"</td>"+
                                    "<td>"+
                                      "<a class='btn btn-default' href='edit.html'>Edit</a>"+
                                      "<button class='btn btn-danger deleteInTable' onclick='deleteUser(this)'>Delete</button>"+
                                    "</td>"+
                                    "<td style='display:none'>" + user.id + "</td>"+
                                  "</tr>";
          } else{
            table[0].innerHTML += "<tr>"+
                                    "<td>"+user.name+"</td>"+
                                    "<td>"+user.email+"</td>"+
                                    "<td>"+date+"</td>"+
                                  "</tr>";
          }
        }
        ii++;
      })
    })
  }

  function updatePagesTable(){
    $.ajax({
      url: "http://localhost:3000/posts",
      method: 'GET',
      dataType: 'json'
    }).done(function(data){
      var table = $("table.pagesTable")
      $.each(data,function(i, page){
        if (page.created == "" || page.created == null){
          var date = "21 Jun 2017"
        } else {
          var date = moment(page.created, "DD/MM/YYYY", true).format("DD MMM YYYY"); // Format the date in DD MMM YYYY format
        }

        if (page.published == "YES"){
          var glyphicon = " glyphicon-ok"
        } else {
          var glyphicon = " glyphicon-remove"
        }

        table[0].innerHTML += "<tr>"+
                                "<td>"+getXWords(page.title, 8)+"</td>"+
                                "<td><span class='glyphicon"+glyphicon+" aria-hidden='true'></span></td>"+
                                "<td>"+date+"</td>"+
                                "<td>"+
                                  "<a class='btn btn-default' href='edit.html'>Edit</a>"+
                                  "<button class='btn btn-danger deleteInTable' onclick='deletePage(this)'>Delete</button>"+
                                "</td>"+
                                "<td style='display:none'>" + page.id + "</td>"+
                              "</tr>";
      })
    })
  }

  function updatePostsTable(){
    $.ajax({
      url: "http://localhost:3000/comments",
      method: 'GET',
      dataType: 'json'
    }).done(function(data){
      var table = $("table.postsTable")
      $.each(data,function(i, post){
        if (post.created == "" || post.created == null){
          var date = "21 Jun 2017"
        } else {
          var date = moment(post.created, "DD/MM/YYYY", true).format("DD MMM YYYY"); // Format the date in DD MMM YYYY format
        }

        if (post.published == "YES"){
          var glyphicon = " glyphicon-ok"
        } else {
          var glyphicon = " glyphicon-remove"
        }

        table[0].innerHTML += "<tr>"+
                                "<td style='max-width: 200px;'>"+getXWords(post.name, 4)+"</td>"+
                                "<td><span class='glyphicon"+glyphicon+" aria-hidden='true'></span></td>"+
                                "<td>"+date+"</td>"+
                                "<td>"+
                                  "<a class='btn btn-default' href='edit.html'>Edit</a>"+
                                  "<button class='btn btn-danger deleteInTable' onclick='deletePost(this)'>Delete</button>"+
                                "</td>"+
                                "<td style='display:none'>" + post.id + "</td>"+
                              "</tr>";
      })
    })
  }


  // Modal Submits

  // Add Page
  $("#addPageForm").submit(function(e){
    e.preventDefault();

    var title = $("#title").val();
    var body = pageBody.getData();
    var url = $(this).attr("action");
    if ($("#published").checked) {
      var published = "YES"
    } else {
      var published = "NO"
    }
    var created = moment().format("DD/MM/YYYY");

    $.post(url,{title:title, body:body, published:published, created:created}).done(function(data){
      var noty = {
        message: "Page has been created successfully",
        cause: 'addPage',
        type: 'success'
      }

      var showNotys = localStorage.getItem('showNoty');
      if(showNotys === null || showNotys == ''){
        var showNotys = [];
      } else {
        var showNotys = JSON.parse(localStorage.getItem('showNoty'));
      }
      showNotys.push(noty);
      localStorage.setItem('showNoty', JSON.stringify(showNotys));
    });
  });

  // Add Post
  $("#addPostForm").submit(function(e){
    e.preventDefault();

    var name = $("#title").val();
    var body = postBody.getData();
    var url = $(this).attr("action");
    if ($("#published").checked) {
      var published = "YES"
    } else {
      var published = "NO"
    }
    var created = moment().format("DD/MM/YYYY");

    $.post(url,{name:name, body:body, published:published, created:created}).done(function(data){
      var noty = {
        message: "Post has been created successfully",
        cause: 'addPost',
        type: 'success'
      }

      var showNotys = localStorage.getItem('showNoty');
      if(showNotys === null || showNotys == ''){
        var showNotys = [];
      } else {
        var showNotys = JSON.parse(localStorage.getItem('showNoty'));
      }
      showNotys.push(noty);
      localStorage.setItem('showNoty', JSON.stringify(showNotys));
    });
  });

  // Add User
  $("#addUserForm").submit(function(e){
    e.preventDefault();

    var name = $("#firstName").val() + " " + $("#surname").val();
    var username = $("#username").val();
    var password = $("#password").val();
    var email = $("#email").val();
    var website = $("website").val();
    var url = $(this).attr("action");
    var joined = moment().format('DD/MM/YYYY');

    if (name && username && password && email){
      $.post(url,{name:name, username:username, email:email, website:website, joined:joined}).done(function(data){

        var noty = {
          message: "User has been created successfully",
          cause: 'addUser',
          type: 'success'
        }

        var showNotys = localStorage.getItem('showNoty');
        if(showNotys === null || showNotys == ''){
          var showNotys = [];
        } else {
          var showNotys = JSON.parse(localStorage.getItem('showNoty'));
        }
        showNotys.push(noty);
        localStorage.setItem('showNoty', JSON.stringify(showNotys));
      });
    }
  });

  getNumberOfPosts();
  getNumberOfPages();
  getNumberOfUsers();
  getNumberOfVisitors();
  updateProgressBars();
  showNoty();

  var currPage = window.location.href;
  currPage = currPage.split("/");
  var cPage = currPage[currPage.length-1];
  switch(cPage){
    case "index.html":
      updateUsersTable(false);
      break;
    case "pages.html":
      updatePagesTable();
      break;
    case "posts.html":
      updatePostsTable();
      break;
    case "users.html":
      updateUsersTable(true);
      break;
    default:
      console.log("New page viewed");
  }
};
function addNewNoty(noty){
  var showNotys = localStorage.getItem('showNoty');
  if(showNotys === null || showNotys == ''){
    var showNotys = [];
  } else {
    var showNotys = JSON.parse(localStorage.getItem('showNoty'));
  }
  showNotys.push(noty);
  localStorage.setItem('showNoty', JSON.stringify(showNotys));
}

function deleteUser(e){
  var modal = $('#confirmDeletion');
  var name = e.parentNode.parentNode.children[0].innerText;
  var id = e.parentNode.parentNode.children[4].innerText;
  modal.modal('show');
  modal[0].children[0].children[0].children[0].children[2].innerText = id;
  modal[0].children[0].children[0].children[1].innerHTML = "<p>Please confirm you would like to delete the following user:</p><p>" + name + "</p>"+
  "<p>This action can NOT be reversed</p>";
}

function confirmUserDeletion(){
  var modal = $('#confirmDeletion');
  var id = modal[0].children[0].children[0].children[0].children[2].innerText;

  $.ajax('http://localhost:3000/users?id=' + id).then(function(data){

    if(data == null || data == ""){
      addNewNoty({
        message: "User was unable to be located, please try refreshing the page",
        cause: "confirmUserDeletion",
        type: "error"
      });
      location.reload();
    } else {
      var url = "http://localhost:3000/users/" + id;
      $.ajax(url,{
        method: 'DELETE'
      }).then(function (){
        addNewNoty({
          message: "User has been deleted successfully",
          cause: 'confirmUserDeletion',
          type: 'success'
        });
      }, function (data){
        addNewNoty({
          message: "User was unable to be deleted, please try again",
          cause: "confirmUserDeletion",
          type: "error"
        });
      });
    }
  });
}

function deletePost(e){
  var modal = $('#confirmDeletion');
  var name = e.parentNode.parentNode.children[0].innerText;
  var id = e.parentNode.parentNode.children[4].innerText;
  modal.modal('show');
  modal[0].children[0].children[0].children[0].children[2].innerText = id;
  modal[0].children[0].children[0].children[1].innerHTML = "<p>Please confirm you would like to delete the following post:</p><p>" + name + "</p>"+
  "<p>This action can NOT be reversed</p>";
}

function confirmPostDeletion(){
  var modal = $('#confirmDeletion');
  var id = modal[0].children[0].children[0].children[0].children[2].innerText;

  $.ajax('http://localhost:3000/comments?id=' + id).then(function(data){

    if(data == null || data == ""){
      addNewNoty({
        message: "Post was unable to be located, please try refreshing the page",
        cause: "confirmPostDeletion",
        type: "error"
      });
      location.reload();
    } else {
      var url = "http://localhost:3000/comments/" + id;
      $.ajax(url,{
        method: 'DELETE'
      }).then(function (){
        addNewNoty({
          message: "Post has been deleted successfully",
          cause: 'confirmPostDeletion',
          type: 'success'
        });
      }, function (data){
        addNewNoty({
          message: "Post was unable to be deleted, please try again",
          cause: "confirmPostDeletion",
          type: "error"
        });
      });
    }
  });
}

function deletePage(e){
  var modal = $('#confirmDeletion');
  var name = e.parentNode.parentNode.children[0].innerText;
  var id = e.parentNode.parentNode.children[4].innerText;
  modal.modal('show');
  modal[0].children[0].children[0].children[0].children[2].innerText = id;
  modal[0].children[0].children[0].children[1].innerHTML = "<p>Please confirm you would like to delete the following page:</p><p>" + name + "</p>"+
  "<p>This action can NOT be reversed</p>";
}

function confirmPageDeletion(){
  var modal = $('#confirmDeletion');
  var id = modal[0].children[0].children[0].children[0].children[2].innerText;

  $.ajax('http://localhost:3000/posts?id=' + id).then(function(data){

    if(data == null || data == ""){
      addNewNoty({
        message: "Page was unable to be located, please try refreshing the page",
        cause: "confirmPageDeletion",
        type: "error"
      });
      location.reload();
    } else {
      var url = "http://localhost:3000/posts/" + id;
      $.ajax(url,{
        method: 'DELETE'
      }).then(function (){
        addNewNoty({
          message: "Page has been deleted successfully",
          cause: 'confirmPageDeletion',
          type: 'success'
        });
      }, function (data){
        addNewNoty({
          message: "Page was unable to be deleted, please try again",
          cause: "confirmPageDeletion",
          type: "error"
        });
      });
    }
  });
}