// Default Noty Notification in top-right corner of screen
function defaultNoty(){
  new Noty({
    text: 'Default Notification - Click to acknowledge me',
  }).show();
}

// Success Noty Notification
function successNoty(){
  new Noty({
    text: 'Success Notification - Click to acknowledge me',
    theme: 'relax',
    type: 'success',
    closeWith: ['click','button']
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