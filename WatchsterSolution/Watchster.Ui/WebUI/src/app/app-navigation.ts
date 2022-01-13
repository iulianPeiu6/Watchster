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
    text: 'Movies',
    icon: 'folder',
    items: [
      {
        text: 'All',
        path: '/movies'
      },
      {
        text: 'New',
        path: '/new-movies'
      },
      {
        text: 'Popular',
        path: '/popular-movies'
      },
      {
        text: 'Recommendations',
        path: '/recommendations'
      }
    ]
  }
];
