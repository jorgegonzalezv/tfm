import os

CHANGE = {
    0: "0",
    1: "8"
}
def process_file(file_path):
    with open(file_path, 'r') as file:
        lines = file.readlines()

    with open(file_path, 'w') as file:
        for line in lines:
            if line.strip():  # Check if the line is not empty
                modified_line = CHANGE[int(line[0])] + line[1:]
                file.write(modified_line)

def main(directory_path):
    print(directory_path)
    for filename in os.listdir(directory_path):
        if filename.endswith(".txt"):
            file_path = os.path.join(directory_path, filename)
            process_file(file_path)
            print(f"Processed: {filename}")

if __name__ == "__main__":
    target_directory = "datasets/seadronesee_cross_coco/size_5/labels/train"
    main(target_directory)

    target_directory = "datasets/seadronesee_cross_coco/size_10/labels/train"
    main(target_directory)

    target_directory = "datasets/seadronesee_cross_coco/size_20/labels/train"
    main(target_directory)

    target_directory = "datasets/seadronesee_cross_coco/size_50/labels/train"
    main(target_directory)

    target_directory = "datasets/seadronesee_cross_coco/size_100/labels/train"
    main(target_directory)
