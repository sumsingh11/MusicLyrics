# MUSIC LYRICS CMS

## Project Overview

The Music Lyrics CMS is a web-based content management system designed for musicians, lyricists, and fans to create, manage, and share song lyrics efficiently. This platform enables users to browse lyrics by artist or album, upvote their favorite lyrics, and leave comments on songs. Musicians and lyricists can add new lyrics, edit existing ones, and categorize them under albums or genres.

Admins have full control over the system, ensuring that lyrics are properly linked to artists and albums while preventing duplicate or incorrect submissions. The CMS also supports features like:

- Searching for lyrics
- Filtering by genre
- Viewing the most popular or trending lyrics based on user engagement

## Database Design

The relational database structure for this CMS follows a well-defined schema representing relationships between artists, albums, and songs.

### Entity Relationship Summary

| Entities →        Relationships →          Description |
|----------------|-----------------|----------------------------------------|
| Artists → Lyrics | One-to-Many (1 → ∞) | A single artist can create multiple songs. |
| Songs → Albums | Many-to-One (∞ → 1) | Multiple songs can belong to a single album. |
| Artists → Albums | One-to-Many (1 → ∞) | An artist can release multiple albums. |

## Wireframes

The system includes various wireframes for key management pages:

- User Management & Update User Page (Users Entity)
- Update Artist Page (Artists Entity)
- Update Album Page (Album Entity)
- Update Songs Page (Songs Entity)

## API Methods (MVP)

The system exposes a set of API endpoints to facilitate interactions with the database:

### Artist Endpoints

- **GET** /api/artists - Retrieve a list of all artists
- **GET** /api/artists/{id} - Retrieve details of a specific artist
- **POST** /api/artists - Add a new artist
- **PUT** /api/artists/{id} - Update artist information
- **DELETE** /api/artists/{id} - Remove an artist

### Album Endpoints

- **GET** /api/albums - Retrieve all albums
- **GET** /api/albums/{id} - Get album details
- **POST** /api/albums - Add a new album
- **PUT** /api/albums/{id} - Update an album
- **DELETE** /api/albums/{id} - Remove an album

### Song Endpoints

- **GET** /api/songs - Retrieve all songs
- **GET** /api/songs/{id} - Retrieve song details
- **POST** /api/songs - Add a new song
- **PUT** /api/songs/{id} - Update a song
- **DELETE** /api/songs/{id} - Remove a song

## Features

- User authentication & role management
- CRUD operations for artists, albums, and songs
- Lyrics browsing & searching
- Upvoting and commenting on lyrics
- Admin controls for content moderation

## Technology Stack

- **Backend**: .NET Core, Entity Framework
- **Database**: SQL Server
- **Frontend**: ASP.NET MVC, React/Angular (Future Enhancements)
- **Authentication**: Identity Framework, JWT

## Future Enhancements

- User authentication & roles
- Real-time lyrics collaboration
- Integration with music streaming APIs
- AI-generated lyric suggestions

## Contributing

If you'd like to contribute, please submit a pull request or open an issue.
