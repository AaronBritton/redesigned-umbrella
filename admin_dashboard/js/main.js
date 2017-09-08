function documentReady(){

  function Post(id, postId, title, email, body){
    this.id = id;
    this.postId = postId;
    this.name = title;
    this.email = email;
    this.body = body;
  }

  function getNumberOfPosts(){  
    $.ajax({
      url: "https://jsonplaceholder.typicode.com/comments",
      method: 'GET',
      dataType: 'json'
    }).done(function(data){
      var posts = new Array();
      $.each(data, function(i, post){
        posts.push(new Post(post.id, post.userId, post.title, post.email, post.body));
      })
      var postBadge = $('span#numOfPosts');
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
      url: "https://jsonplaceholder.typicode.com/posts",
      method: 'GET',
      dataType: 'json'
    }).done(function(data){
      var pages = new Array();
      $.each(data, function(i, page){
        pages.push(new Page(page.id, page.userId, page.title, page.body));
      })
      var pageBadge = $('span#numOfPages');
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
      url: "https://jsonplaceholder.typicode.com/users",
      method: 'GET',
      dataType: 'json'
    }).done(function(data){
      var users = new Array();
      $.each(data, function(i, user){
        users.push(new User(user.id, user.name, user.username, user.email, user.website));
      })
      var userBadge = $('span#numOfUsers');
      $.each(userBadge, function(i, badge){
        badge.innerHTML = users.length;
      });

    })
  };
  

  getNumberOfPosts();
  getNumberOfPages();
  getNumberOfUsers();
};