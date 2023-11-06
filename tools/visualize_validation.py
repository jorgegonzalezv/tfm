"""
Visualize yolo validation results.
"""
import argparse
import os
import matplotlib.pyplot as plt
import matplotlib.patches as patches
import pandas as pd

COLUMNS = ["label", "xmin", "ymin", "h", "w", "threshold"]
PRETRAINED_COCO_PREDICTIONS = "lib/yolov5-master/runs/val/saved/coco_pretrained_sds/labels"
def parse_options():
    parser = argparse.ArgumentParser()
    parser.add_argument("--prediction", help="prediction json/.txt files")
    parser.add_argument("--gt", default="datasets/seadronesee/labels/val", help="input dataset image files")
    parser.add_argument("--images", default="datasets/seadronesee/images/val", help="input dataset image files")
    parser.add_argument("--threshold", default=0.3, help="threshold confidence")
    return parser.parse_args()


LISTA = [2887, 5523, 5494, 5422, 4735, 4719, 4161, 3791, 3727, 1136, 339, 1414, 5428]
def main(args):
    for filename in sorted(os.listdir(args.images)):
        if int(filename.split(".")[0]) not in LISTA:
             continue
        gt_filename = os.path.join(args.gt, filename.split(".png")[0] + ".txt")
        prediction_filename = os.path.join(args.prediction, filename.split(".png")[0] + ".txt")
        prediction_coco_filename = os.path.join(PRETRAINED_COCO_PREDICTIONS, filename.split(".png")[0] + ".txt")
        image_filename = os.path.join(args.images, filename)

        im = plt.imread(image_filename)
        im_h, im_w, _ = im.shape
    
        # pred: filter by confidence threshold 
        labels = pd.read_csv(prediction_filename, names=COLUMNS, header=None, sep=" ")
        labels = labels[labels.threshold >= args.threshold]

        # gt:
        labels_gt = pd.read_csv(gt_filename, names=COLUMNS[:-1], header=None, sep=" ")

        labels_coco = pd.read_csv(prediction_coco_filename, names=COLUMNS, header=None, sep=" ")
        labels_coco = labels_coco[labels_coco.threshold >= args.threshold]

        # display im in subplot figure
        fig, axs = plt.subplots(1, 3, figsize=(15, 10))
        plt.title(filename)
        for ax, _labels, color in zip(axs, [labels, labels_gt, labels_coco], ["b","g","r"]):
            ax.imshow(im)
            for _, row in _labels.iterrows():
                label = row[0]
                xmin, ymin, w, h = row[1:5]
                originX = (xmin - w/2) * im_w
                originY = (ymin - h/2) * im_h
                h = h * im_h
                w = w * im_w
                rect = patches.Rectangle((originX, originY), w, h, linewidth=1, edgecolor=color, facecolor='none') # , label=label)
                ax.add_patch(rect)

        plt.tight_layout()
        # plt.legend()
        plt.waitforbuttonpress(0)
        plt.close()

if __name__ == "__main__":
    args = parse_options()
    main(args)
