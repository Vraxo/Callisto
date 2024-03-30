import os

def merge_files(directory):
    # Get list of all files in the directory and its subdirectories
    file_list = []
    for root, _, files in os.walk(directory):
        for file in files:
            file_list.append(os.path.join(root, file))

    # Create a new text file to store merged contents
    with open('merged_files.txt', 'w') as merged_file:
        for file_path in file_list:
            with open(file_path, 'r') as current_file:
                # Write filename as a comment
                merged_file.write(f'// File: {os.path.basename(file_path)}\n\n')
                # Write file contents
                merged_file.write(current_file.read())
                # Add padding between files
                merged_file.write('\n\n' + '-'*40 + '\n\n')

if __name__ == "__main__":
    # Get the current directory
    current_directory = os.getcwd()
    merge_files(current_directory)
    print("Files merged successfully.")
