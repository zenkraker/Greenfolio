# Use Microsoft's official Dev Container image for .NET 9
FROM mcr.microsoft.com/devcontainers/dotnet:9.0

# Install curl & unzip for package management
RUN apt-get update && apt-get install -y curl unzip \
    && rm -rf /var/lib/apt/lists/*

# Install Node.js & npm properly
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g @angular/cli && \
    npm install -g corepack && corepack enable

# Install pnpm globally
RUN corepack prepare pnpm@latest --activate

# Set the default user to vscode
USER vscode
