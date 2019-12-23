# ThePLeague-api
API for the [ThePLeague WebApp](https://github.com/omikolaj/the-p-league). Uses number of different API design patterns such as Ports, Adapter, Supervisor and Repository patterns. The API is architected with three layers, API, Domain and Data.

## Features and Technologies

- API implements a Refresh Token route
- Entity Framework Core for database communication
- SQL Server database
- Integrates Cloudinary API for persisting images
- The API uses MailKit nuget package to send e-mail notifications

## Usage

This API has been deployed to Azure and is consumed by the front-end web app hosted live at https://pleaguefunc.azurewebsites.net. Run npm install and ng serve to install all of the dependencies and start the front end server for local development.

## Contributing
Bug reports and pull requests are welcome on GitHub at https://github.com/omikolaj/the-p-league-api. This project is intended to be a safe, welcoming space for collaboration, and contributors are expected to adhere to the [Contributor Covenant](http://contributor-covenant.org) code of conduct.

## License
This project is available as open source under the terms of the [MIT License](https://opensource.org/licenses/MIT)
