Noty.overrideDefaults({
  callbacks: {
    onTemplate: function() {
      if (this.options.type === 'reply'){
        this.barDom.innerHTML = '<div class="my-custom-template noty_body">';
        this.barDom.innerHTML += '<div class="noty-header">' + this.options.category + '</a> <span class="noty-bull">&bull;</span></div>';
        this.barDom.innerHTML += '<h3 class="from-name">' + this.options.from + '</h3>';
        this.barDom.innerHTML += '<p class="noty-reply">' + this.options.text + '</p>';
        this.barDom.innerHTML += '<img src="' + this.options.avatar + '">';
        this.barDom.innerHTML += '<div>';
      }
    }
  }
})

// Default Noty Notification in top-right corner of screen
function defaultNoty(){
  new Noty({
    text: 'Default Notification - Click to acknowledge me',
  }).show();
}

// Success Noty Notification in top-right corner of screen
function successNoty(){
  new Noty({
    text: 'Success Notification - Click to acknowledge me',
    theme: 'relax',
    type: 'success',
    closeWith: ['click','button']
  }).show();
}

// Success Noty Notification in top-right corner of screen (with added timeout after 3 seconds)
function successTimeoutNoty(){
  new Noty({
    text: 'Success Notification - I timeout after 3 seconds',
    theme: 'relax',
    type: 'success',
    timeout: 3000,
    closeWith: ['click', 'button']
  }).show();
}

// Error Noty Notification in top-right of screen
function errorNoty(){
  new Noty({
    text: "Error Notification - Click to acknowledge me",
    type: 'error',
    closeWith: ['click', 'button']
  }).show();
}

// Timeout Noty Notification in the top-right corner of screen
function timeouttrNoty(){
  new Noty({
    text: 'Timeout Notification',
    layout: 'topRight',
    timeout: 3000
  }).show();
}

// Noty Notification (with added Image) in the top-right corner of screen
function imageNoty(){
  new Noty({
    text: 'Image Notification',
    layout: 'topRight',
    type: 'reply',
    category: 'Friend Request',
    date: Date.now(),
    from: 'Dr Patrick Wang',
    avatar: 'https://avatars3.githubusercontent.com/u/271912?v=4&s=460'
  }).show();
}