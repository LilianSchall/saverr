{ pkgs ? import <nixpkgs> {} }:
(pkgs.mkShell {
  name = "autotrade";
  nativeBuildInputs = with pkgs; [
      python313
      python313Packages.numpy
      nodePackages.prettier
      nodejs
      dotnetCorePackages.dotnet_9.sdk
      dotnet-ef
  ];
  shellHook = ''
  echo "Welcome to this Nix Shell:";
  echo "- Node.js $(node --version)";
  echo "- npm $(npm --version)";
  alias prune-branches="git branch | cat | grep -E '(fix|feat|chore)/*' | xargs git branch -D";
  '';
})
