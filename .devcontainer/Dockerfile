# Use the official .NET SDK image as the base
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base

# Set the SHELL environment variable to bash
ENV SHELL=/bin/bash

# Install dependencies
RUN apt-get update && \
    apt-get install -y curl gnupg && \
    curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g pnpm && \
    pnpm setup && \
    export PNPM_HOME="/root/.local/share/pnpm" && \
    export PATH="$PNPM_HOME:$PATH" && \
    pnpm add -g @angular/cli

# Set the working directory for the backend
WORKDIR /backend

# Copy the backend project files
COPY backend/ .

# Verify the .sln file exists (for debugging)
RUN ls -la /backend

# Restore backend dependencies (run at the solution level)
RUN dotnet restore Greenfolio.API.sln

# Set the working directory for the frontend
WORKDIR /frontend

# Copy the frontend project files
COPY frontend/ .

# Expose ports for backend (5000) and frontend (4200)
EXPOSE 5000 4200

# Install frontend dependencies
RUN pnpm install

# Default command (optional)
CMD ["bash"]