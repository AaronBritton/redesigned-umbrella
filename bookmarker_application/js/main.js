// Listen for form submission
document.getElementById('form').addEventListener('submit', saveBookmark);

// Save bookmark
function saveBookmark(e){
  // Get form values
  var siteName = document.getElementById('siteName').value;
  var siteUrl = document.getElementById('siteUrl').value;

  if(!validateForm(siteName, siteUrl)){
    return false;
  }

  var bookmark = {
    name: siteName,
    url: siteUrl
  }

  /*
    // Local storage Test
    localStorage.setItem('test','Hello World');
    console.log(localStorage.getItem('test'));
    localStorage.removeItem('test');
    console.log(localStorage.getItem('test'));
  */
  
  if(localStorage.getItem('bookmarks') === null){
    var bookmarks = [];
  } else {
    var bookmarks = JSON.parse(localStorage.getItem('bookmarks'));
  }
  bookmarks.push(bookmark);
  localStorage.setItem('bookmarks', JSON.stringify(bookmarks));

  document.getElementById('form').reset();

  getBookmarks();

  // Prevent form from submitting
  e.preventDefault();
}

// Get Bookmarks
function getBookmarks(){
  var bookmarks = JSON.parse(localStorage.getItem('bookmarks'));

  var bookmarksResults = document.getElementById('bookmarksResults');

  // Build outputs
  bookmarksResults.innerHTML = '';
  for(var i = 0; i < bookmarks.length; i++){
    var name = bookmarks[i].name;
    var url = bookmarks[i].url;

    bookmarksResults.innerHTML += '<div class="well">'+
                                    '<h3>'+name+
                                      '<a class="btn btn-default" target="_black" href="'+url+'">Visit</a>'+
                                      '<a onclick="deleteBookmark(\''+url+'\')" class="btn btn-danger" href="#">Delete</a>'+
                                    '</h3>'
                                  '</div>';
  };
}

// Delete Bookmark
function deleteBookmark(url){
  var bookmarks = JSON.parse(localStorage.getItem('bookmarks'));
  
  for(var i = 0; i < bookmarks.length; i++){
    if (bookmarks[i].url == url){
      // Remove the current bookmark
      bookmarks.splice(i, 1);
    }
  }
  localStorage.setItem('bookmarks', JSON.stringify(bookmarks));

  getBookmarks();
}

function validateForm(siteName, siteUrl){

  if(!siteName || !siteUrl){
    alert('Please fill in the form');
    return false;
  }

  var expression = /[-a-zA-Z0-9@:%_\+.~#?&//=]{2,256}\.[a-z]{2,4}\b(\/[-a-zA-Z0-9@:%_\+.~#?&//=]*)?/gi;
  var regex = new RegExp(expression);

  if (!siteUrl.match(regex)) {
    alert('Please enter a valid URL');
    return false;
  }

  return true;
}