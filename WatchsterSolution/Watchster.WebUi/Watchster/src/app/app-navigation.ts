export const navigation = [
  {
    text: 'Home',
    path: '/home',
    icon: 'home'
  },
  {
    text: 'Me',
    icon: 'user',
    items: [
      {
        text: 'Profile',
        path: '/profile'
      }
    ]
  },
  {
    text: 'Features',
    icon: 'folder',
    items: [
      {
        text: 'Movies',
        path: '/movies'
      },
      {
        text: 'Recommendations',
        path: '/recommendations'
      }
    ]
  }
];
