"""
Create reduced cross-training Sea Drone Sea train dataset.

"""

import os
import shutil
import random 

SEED = 2023
SEADRONESEE_DIR = os.path.join("datasets","seadronesee", "labels", "train")
DATASET_SIZES = [5, 10, 20, 50, 100]
# DATASET_SIZES = [10, 20]

DATASET_DIRNAME = os.path.join("datasets", "seadronesee_cross_coco")
random.seed(SEED)

def main():
    # create dirs
    if not os.path.isdir(DATASET_DIRNAME):
        os.makedirs(DATASET_DIRNAME)

    for size in DATASET_SIZES:
        dirname = os.path.join(DATASET_DIRNAME, "size_{}".format(size))
        if not os.path.isdir(dirname):
            os.makedirs(dirname)
            os.makedirs(os.path.join(dirname, "images", "train"))
            os.makedirs(os.path.join(dirname, "labels", "train"))
            shutil.copy(os.path.join(SEADRONESEE_DIR,"../..","data_tfm.yaml"), dirname)

    # read sds
    files = os.listdir(SEADRONESEE_DIR)
    files = sorted(files, key=lambda x: int(x.split(".txt")[0]))

    # select random image/labels
    labels = []
    for size in DATASET_SIZES:
        _labels = []
        for _ in range(size):
            _labels.append(random.choice(files))
        labels.append(_labels)

    # copy them to new dataset
    for dataset in labels:
        dirname = os.path.join(DATASET_DIRNAME, "size_{}".format(len(dataset)))
        for label in dataset:
            filename = os.path.join(SEADRONESEE_DIR, label)
            imagename = filename.replace("labels", "images").replace("txt", "png")
            shutil.copy(filename, os.path.join(dirname, "labels", "train"))
            shutil.copy(imagename, os.path.join(dirname, "images", "train"))

if __name__ == "__main__":
    main()